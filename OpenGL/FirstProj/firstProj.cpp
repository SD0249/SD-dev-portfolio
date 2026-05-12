// There are many different versions of OpenGL drivers
// The location of most of its functions is not known at compile-time
// and needs to be queried at run-time. 
// Developer needs to retrieve the location of the function -> store it in function pointers for later use.
// GLAD does this for us!

// Be sure to include GLAD before GLFW.
// The include file for GLAD includes
// the required OpenGL headers behind the scenes(like GL/gl.h)
// so be sure to include GLAD before other header files 
// that require OpenGL(like GLFW)
#include <glad/glad.h>				// GL Aoader-Denerator, a tool used to manage OpenGL function pointers
#include <GLFW/glfw3.h>				// GL FrameWork, a library that manages creating windows, OpenGL contexts, and handling input for graphics applications

#include <iostream>
using namespace std;

// ** HELPER FUNCTION **
void framebuffer_size_callback(GLFWwindow* window, int width, int height);
void processInput(GLFWwindow* window);

// Settings
const unsigned int SCR_WIDTH = 800;
const unsigned int SCR_HEIGHT = 600;

// --> SOURCE CODE for Vertex Shader
// Usually the input data is not already in NDC(Normalized Device Coordinates) 
// so first transforms the input data to coordinates that fall within OpenGL's visible region (NDC)
const char* vertexShaderSource = "#version 330 core\n"
								 "layout (location = 0) in vec3 aPos;\n"		// Input vertex attributes with 'in' keyword
																				// Specify the LOCATION of the 'position' vertex ATTRIBUTE in the vertex shader with layout (location = 0)
								 "void main()\n"
								 "{\n"
								 "    gl_Position = vec4(aPos.x, aPos.y, aPos.z, 1.0);\n"	// Assign the position data to the predefined gl_Position variable(vec4)
								 "}\0";	// At the end of the main function, whatever gl_Position is set to will be used as the OUTPUT of the vertex shader


// --> SOURCE CODE for Fragment Shader
// All about calculating the color output of your pixels
// NOTE: Colors in computer graphics are represented as an array of 4 values:
//		 the red, green, blue and alpha (opacity) component, commonly abbreviated to RGBA.
const char* fragmentShaderSource = "#version 330 core\n"
								   "out vec4 FragColor;\n"	// Final color output that we should calculate ourselves (out keyword - output)
								   "void main()\n"
								   "{\n"
								   "    FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);\n"	// This fragment shader will always output an orange-ish color
								   "}\0";

int main() {

	// ---------------------------------------------------------------------
	// Setting up glfw
	// ---------------------------------------------------------------------
	glfwInit();	// Initializes GLFW Library

	// glfwWindowHint
	// First Argument --> What option we want to configure BEFORE creating a window or initializing a library
	// Second Argument --> Integer that sets the value of our option

	// Using OpenGL version 3.3 (major.minor)
	glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
	glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);

	// Tell GLFW to explicitly use 'core-profile' (access to smaller subset of OpenGL features)
	glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);
	// glfwWindowHint(GLFW_OPENGL_FORWARD_COMPAT, GL_TRUE);


	// ---------------------------------------------------------------------
	// Create a window
	// "WINDOW is the WHOLE CANVAS"
	// "The VIEWPORT is part OpenGL is ALLOWED to PAINT on"
	// ---------------------------------------------------------------------
	// Create a window object that holds all the windowing data & 
	// is required to most other GLFW's functions

	// A GLFWwindow object is returned (needed for later GLFW operations)
	GLFWwindow* window = glfwCreateWindow(SCR_WIDTH, SCR_HEIGHT, "LearnOpenGL", NULL, NULL);	// Param: Width, Height, Name, Ignore
	if (window == NULL) {
		cout << "Failed to create GLFW window" << endl;
		glfwTerminate();	// Destroys all remaining windows and cursors and frees any other resource
		return -1;
	}
	glfwMakeContextCurrent(window);		// Make the context of this window the main context on the current thread
	glfwSetFramebufferSizeCallback(window, framebuffer_size_callback); // Tell GLFW to CallBack size resize function on every window resize
	// ** Callback Functions
	// Used to register your own functions. Register the callback functions after creation of window and before the render loop is initiated


	// ---------------------------------------------------------------------
	// Use GLAD to retrieve OpenGL functions & Create function pointers
	// ---------------------------------------------------------------------
	// Pass GLAD the function to load the address of the OpenGL function pointers which is OS-specific.
	// glfwGetProcAddress defines the correct function based on which OS we're compiling for
	if (!gladLoadGLLoader((GLADloadproc)glfwGetProcAddress)) {
		cout << "Failed to initialize GLAD" << endl;
		return -1;
	}


	// ---------------------------------------------------------------------
	// Build & Compile Shader Program
	// --> Instructing GPU how it should process the vertex data within a vertex and fragment shader
	// ---------------------------------------------------------------------
	// Vertex shader --> Has to be dymamically compiled at run-time from its source code
	unsigned int vertexShader;
	vertexShader = glCreateShader(GL_VERTEX_SHADER);	// Provide the type of shader 
	// we want to create as an argument to glCreateShader
// Attach shader source code to shader object
	glShaderSource(vertexShader, // Shader object to compile
		1,			 // Specifies how many strings we are passing as a source code (only one) 
		&vertexShaderSource,	// Actual source code of the vertex shader
		NULL);
	glCompileShader(vertexShader);

	// Check if compilation was successful (Compile Time error check)
	int success;		// Success indicator
	char infoLog[512];	// Error Message container
	glGetShaderiv(vertexShader, GL_COMPILE_STATUS, &success);	// Query a shader for information

	if (!success) {	// If failed
		glGetShaderInfoLog(vertexShader, 512, NULL, infoLog);	// Retrieve Error Message
		cout << "ERROR::SHADER::VERTEX::COMPILATION_FAILED\n" << infoLog << endl;
	}


	// Fragment shader
	unsigned int fragmentShader;
	fragmentShader = glCreateShader(GL_FRAGMENT_SHADER);
	glShaderSource(fragmentShader, 1, &fragmentShaderSource, NULL);
	glCompileShader(fragmentShader);

	// Check for fragment shader compile errors
	glGetShaderiv(fragmentShader, GL_COMPILE_STATUS, &success);
	if (!success) {
		glGetShaderInfoLog(fragmentShader, 512, NULL, infoLog);
		cout << "ERROR::SHADER::FRAGMENT::COMPILATION_FAILED\n" << infoLog << endl;
	}

	// Shader Program Object --> Final linked version of multiple shaders combined
	//							 Link compiled shaders to a shader program and 
	//							 ACTIVATE this program when rendering objects (Will be used when we issue render calls)
	// Create a program object
	unsigned int shaderProgram;
	shaderProgram = glCreateProgram();	// Creates a program and returns the ID reference to the newly created program object

	// Attach the previously compiled shaders to the program object
	glAttachShader(shaderProgram, vertexShader);
	glAttachShader(shaderProgram, fragmentShader);
	glLinkProgram(shaderProgram);					// Links all attached shaders to one final shader program object

	// Check for shader program linking errors
	glGetProgramiv(shaderProgram, GL_LINK_STATUS, &success);
	if (!success) {
		glGetProgramInfoLog(shaderProgram, 512, NULL, infoLog);
		cout << "ERROR::SHADER::PROGRAM::LINKING_FAILED\n" << infoLog << endl;
	}

	// Make sure to delete the shader object once linking them to the project - Don't need them anymore
	// (Make good use of memory)
	glDeleteShader(vertexShader);
	glDeleteShader(fragmentShader);


	// ---------------------------------------------------------------------
	// Set up VERTEX data (and buffer(s)) and configure vertex attributes
	// NOTE: All coordinates within normalized device coordinates range 
	//		 (-1.0 - 1.0) will end up visible on your screen
	// --> Sending input vertex data to the GPU
	// ---------------------------------------------------------------------
	// ** Initialization code (done once (unless your object frequently changes)
	float vertices[] = {
		// ERROR: There are overlapping vertices! Use Element Buffer Object in this case!
		// Store only the unique vertices and then specify the order at which we want to draw these vertices in --> Indexed Drawing
		// First Triangle
		// 0.5f,  0.5f, 0.0f,	 // Top right
		// 0.5f, -0.5f, 0.0f,  // Bottom right
		//-0.5f,  0.5f, 0.0f,  // Top left
		// Second Triangle
		// 0.5f, -0.5f, 0.0f,	 // Bottom right
		//-0.5f, -0.5f, 0.0f,	 // Bottom left
		//-0.5f,  0.5f, 0.0f	 // Top left

		 0.5f,  0.5f, 0.0f,		// Top right
		 0.5f, -0.5f, 0.0f,		// Bottom right
		-0.5f, -0.5f, 0.0f,		// Bottom left
		-0.5f,  0.5f, 0.0f		// Top left
	};	// The 'depth' of the triangle remains the same, making it look like it's 2D. 

	unsigned int indices[] = {	// Note that we start from 0!
		0, 1, 3,	// first triangle
		1, 2, 3		// Second triangle
	};

	// Generate a vertext buffer object (VBO) with a buffer ID
	unsigned int VBO;
	glGenBuffers(1, &VBO);

	// Generate a EBO
	unsigned int EBO;
	glGenBuffers(1, &EBO);

	// Generate a VAO
	// Core OpenGL requires that we use a VAO so it knows what to do with our vertex inputs. 
	// If we fail to bind a VAO, OpenGL will most likely refuse to draw anything. 
	unsigned int VAO;
	glGenVertexArrays(1, &VAO);		

	// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	// 0. Bind Vertex Array Object (VAO)
	// This function binds a vertex array object. 
	// Any subsequent VBO, EBO, glVertexAttribPointer and glEnableVertexAttribArray
	// calls will be stored inside the VAO currently bound.
	glBindVertexArray(VAO);
	// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	// 1. Copy vertices array in a buffer for OpenGL to use
	// OpenGL has many types of buffer objects. 
	// It allows us to bind to SEVERAL buffers at ONCE 
	// as long as they have a DIFFERENT buffer type.
	glBindBuffer(GL_ARRAY_BUFFER, VBO);	

	// Any call we make on the GL_ARRAY_BUFFER target will be used to configure currently bound buffer
	// Copy user-defined data into the currently bound buffer
	glBufferData(GL_ARRAY_BUFFER,	// Type of the buffer we want to copy data into
				 sizeof(vertices),	// The size of data (in bytes) we want to pass to buffer
				 vertices,			// Actual data we send
				 GL_STATIC_DRAW);	// How we want the graphics card to manage the given data
									// GL_STREAM_DRAW: The data is set only ONCE and used by the GPU at most a FEW times.
									// GL_STATIC_DRAW: The data is set only ONCE and used MANY times.
									// GL_DYNAMIC_DRAW: The data is changed A LOT and used MANY times. 
	
	// Bind EBO and copy the indices into the buffer
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, EBO);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(indices), indices, GL_STATIC_DRAW);

	// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	// 2. Linking vertex Attributes - Setting the Vertex Attribute pointers
	// Tells OpenGL how it should INTERPRET the vertex data in memory (PER vertex attribute)
	// and how it should CONNECT the vertex data to the vertex shader's attributes
	glVertexAttribPointer(0,				// Specifies the index of the vertex attribute
						  3,				// Specifies the number of components per Vertex Attribute. Must be 1, 2, 3, 4 (vec3 -> 3)
						  GL_FLOAT,			// Type of Data 
					      GL_FALSE,			// If we want the data to be normalized (Clamped to the range -1 to 1 for signed values and 0 to 1 for unsigned values)
						  3 * sizeof(float), // STRIDE - Tells us the SPACE between consecutive vertex attributes in BYTES (Could have been 0 because it is tightly packed)
						  (void*)0);		// Pointer - OFFSET of where the position data begins in the buffer
	// Enable Vertex Attribute with giving the vertex attribute location as its argument
	glEnableVertexAttribArray(0);	// Where each vertex attribute takes its data from is determined by the VBO 
									// currently bound to GL_ARRAY_BUFFER when calling glVertexAttribPointer
	// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	// UNBINDING VBO, EBO, VAO
	// Note that this is allowed, the call to glVertexAttribPointer registered VBO as 
	// the vertex attribute's bound vertex buffer object so afterwards we can safely unbind
	glBindBuffer(GL_ARRAY_BUFFER, 0);
	
	// Remember: Do NOT unbind the EBO while a VAO is active as the bound element buffer object is stored in the VAO; keep the EBO bound.
	// glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);
	
	// You can unbind the VAO afterwards so other VAO calls won't accidentally modify this VAO, 
	// but this rarely happens. Modifying OTHER VAOs requires a call to glBindVertexArray anyways
	// so we generally don't unbind VAOs (nor VBOs) when it is not directly necessary.
	glBindVertexArray(0);
	// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	// Using the Shader program and drawing the object is inside Render Loop's Render portion


	// ---------------------------------------------------------------------
	// Setting GL Viewport
	// ---------------------------------------------------------------------
	// Before we start rendering - Tell OpenGL the size of the rendering window!!
	// How we want to display data and coordinates with respect to the window
	// "ONLY DRAW INSIDE THIS RECTANGULAR REGION OF THE WINDOW"
	
	// ** If OpenGL draws in less space of the window, the rest of the space can be used for GUI panels, Text overlay, toolbars, etc.
	// Normalized Device coordinates to Window Coordinates(Viewport)
	glViewport(0, 0, 800, 600);	// Param: Location of the lower left corner(first two) | Width and height of the RENDERING WINDOW in pixels

	// ** Viewport Transformation 
	// NDC (-1, 1) --> Screen (0, width/height)
	// 
	// screenX = (ndcX + 1) / 2 * width + x_v_min(translation offset)
	// screenY = (ndcY + 1) / 2 * height + y_v_min(translation offset)
	// z_v(depth) = ndcZ(z_far - z_near) + z_n
	// 
	// * ndc_coor + 1 / 2 maps the range [-1, 1] to [0, 1] which then is scaled to viewport width/height
	// * Y Axis inversion - top left corner (0, 0) --> 1 - y_ndc to flip y-axis. (distance from bottom to distance from top)
	//
	// Screen (0, width/height) --> NDC (-1, 1) 
	// ndcX = 2(screenX/width) - 1
	// ndcY = 1 - 2(screenY/width)


	// ---------------------------------------------------------------------
	// RENDER LOOP
	// ---------------------------------------------------------------------
	// Uncomment this call to draw in wireframe polygons
	glPolygonMode(GL_FRONT_AND_BACK,		// Specifies the polygons the mode applies to (FRONT, BACK, FRONT_AND_BACK)
				  GL_LINE);				// Specifies how the polygons will be rasterized (POINT, LINE, FILL)

	while (!glfwWindowShouldClose(window)) 
	{
		// GLFW: INPUT
		// Check for specific key presses and react accordingly every frame
		processInput(window);


		// RENDERING COMMANDS HERE
		// Clear viewport
		glClearColor(0.2f, 0.3f, 0.3f, 1.0f);	// State-Setting Function
		glClear(GL_COLOR_BUFFER_BIT);			// Pass in buffer bits to specify which buffer we would like to clear
		// State-Using function (ex. Uses current state to retrieve the clearing color from)

		// Activate Shader Program object 
		glUseProgram(shaderProgram);		// Every shader & rendering call after glUseProgram will now use this program object
		glBindVertexArray(VAO);
		//glDrawArrays(GL_TRIANGLES,			// OpenGL Primitive Type we'd like to draw
		//			 0,						// The starting index of the vertex array we'd like to draw
		//			 3);					// How many vertices we want to draw, which is 3
		glDrawElements(GL_TRIANGLES,		 
					   6,					// Count or number of elements we'd like to draw
			           GL_UNSIGNED_INT,		// Type of the indices 
					   0);					// Specify an offset in the EBO
		glBindVertexArray(0);			// No need to unbind it everytime though. 

		// GLFW: Check and Call EVENTS and SWAP the BUFFERS
		glfwSwapBuffers(window);	// Swap the color buffer(2D buffer) that is used to render during this render iteration and show it as output to the screen
									// ** Double Buffer
									//    Front buffer - Contains the final output image that is shown at the screen,
									//	  Back buffer  - Rendering commands draw to the back buffer
									//	As soon as ALL the rendering commands are finished, swap the back buffer to the front buffer so the image can be displayed without still being rendered to			
		glfwPollEvents();			// Checks if any events are triggered. Updates window state and calls corresponding functions (registered via callback methods)
	}

	// Optional: De-allocate all resources once they've outlived their purpose:
	glDeleteVertexArrays(1, &VAO);
	glDeleteBuffers(1, &VBO);
	glDeleteBuffers(1, &EBO);
	glDeleteProgram(shaderProgram);

	glfwTerminate();				// Properly clean/delete all of GLFW's resources that we allocated
	return 0; // main function is terminated
}


// ** HELPER FUNCTIONS
// Resize the viewport with user's window resizing
void framebuffer_size_callback(GLFWwindow* window, int width, int height) 
{
	// Make sure the viewport matches the new window dimensions; 
	// Note that width and height will be significantly larger 
	// than specified on retina displays
	glViewport(0, 0, width, height);
}

// Keep code input organized
// Process all input: Query GLFW whether relevant keys are pressed/released
void processInput(GLFWwindow* window)
{
	if (glfwGetKey(window, GLFW_KEY_ESCAPE) == GLFW_PRESS)
		glfwSetWindowShouldClose(window, true);
}