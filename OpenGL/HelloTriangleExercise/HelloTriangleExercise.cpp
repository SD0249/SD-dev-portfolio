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
const char* orangeFragmentShaderSource = "#version 330 core\n"
"out vec4 FragColor;\n"	// Final color output that we should calculate ourselves (out keyword - output)
"void main()\n"
"{\n"
"    FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);\n"	// This fragment shader will always output an orange-ish color
"}\0";

// ****************************************************************************
// Exercise 3 - Create TWO shader programs where the second program 
// uses a different fragment shader that outputs the color yellow
// ****************************************************************************
const char* yellowFragmentShaderSource = "#version 330 core\n"
"out vec4 FragColor;\n"	// Final color output that we should calculate ourselves (out keyword - output)
"void main()\n"
"{\n"
"    FragColor = vec4(1.0f, 1.0f, 0.0f, 1.0f);\n"	// This fragment shader will always output an orange-ish color
"}\0";

int main() {

	// ---------------------------------------------------------------------
	// Setting up glfw
	// ---------------------------------------------------------------------
	glfwInit();	// Initializes GLFW Library

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
	unsigned int orangeFragmentShader;
	orangeFragmentShader = glCreateShader(GL_FRAGMENT_SHADER);
	glShaderSource(orangeFragmentShader, 1, &orangeFragmentShaderSource, NULL);
	glCompileShader(orangeFragmentShader);

	// Check for fragment shader compile errors
	glGetShaderiv(orangeFragmentShader, GL_COMPILE_STATUS, &success);
	if (!success) {
		glGetShaderInfoLog(orangeFragmentShader, 512, NULL, infoLog);
		cout << "ERROR::SHADER::FRAGMENT::COMPILATION_FAILED\n" << infoLog << endl;
	}

	unsigned int yellowFragmentShader;
	yellowFragmentShader = glCreateShader(GL_FRAGMENT_SHADER);
	glShaderSource(yellowFragmentShader, 1, &yellowFragmentShaderSource, NULL);
	glCompileShader(yellowFragmentShader);

	glGetShaderiv(yellowFragmentShader, GL_COMPILE_STATUS, &success);
	if (!success) {
		glGetShaderInfoLog(yellowFragmentShader, 512, NULL, infoLog);
		cout << "ERROR::SHADER::FRAGMENT::COMPILATION_FAILED\n" << infoLog << endl;
	}


	// Shader Program Object --> Final linked version of multiple shaders combined
	//							 Link compiled shaders to a shader program and 
	//							 ACTIVATE this program when rendering objects (Will be used when we issue render calls)
	// Create first shader program object
	unsigned int firstShaderProgram;
	firstShaderProgram = glCreateProgram();	// Creates a program and returns the ID reference to the newly created program object

	// Attach the previously compiled shaders to the program object
	glAttachShader(firstShaderProgram, vertexShader);
	glAttachShader(firstShaderProgram, orangeFragmentShader);
	glLinkProgram(firstShaderProgram);					// Links all attached shaders to one final shader program object

	// Check for shader program linking errors
	glGetProgramiv(firstShaderProgram, GL_LINK_STATUS, &success);
	if (!success) {
		glGetProgramInfoLog(firstShaderProgram, 512, NULL, infoLog);
		cout << "ERROR::SHADER::PROGRAM::LINKING_FAILED\n" << infoLog << endl;
	}

	// Create second shader program object
	unsigned int secondShaderProgram;
	secondShaderProgram = glCreateProgram();
	glAttachShader(secondShaderProgram, vertexShader);
	glAttachShader(secondShaderProgram, yellowFragmentShader);
	glLinkProgram(secondShaderProgram);

	glGetProgramiv(secondShaderProgram, GL_LINK_STATUS, &success);
	if (!success) {
		glGetProgramInfoLog(secondShaderProgram, 512, NULL, infoLog);
		cout << "ERROR::SHADER::PROGRAM::LINKING_FAILED\n" << infoLog << endl;
	}

	// Make sure to delete the shader object once linking them to the project - Don't need them anymore
	// (Make good use of memory)
	glDeleteShader(vertexShader);
	glDeleteShader(orangeFragmentShader);
	glDeleteShader(yellowFragmentShader);


	// ---------------------------------------------------------------------
	// Set up VERTEX data (and buffer(s)) and configure vertex attributes
	// NOTE: All coordinates within normalized device coordinates range 
	//		 (-1.0 - 1.0) will end up visible on your screen
	// --> Sending input vertex data to the GPU
	// ---------------------------------------------------------------------
	// ** Initialization code (done once (unless your object frequently changes)
	// ****************************************************************************
	// Exercise 1 - Try to draw 2 triangles next to each other using glDrawArrays
	// by adding more vertices to your data 
	// ****************************************************************************
	// Exercise 2 - Now create the same 2 triangles using two different VAOs and VBOs for their data
	// --> Errors encountered: 
	//     Run-Time Check Failure #2 - Stack around the variable 'VBO2' was corrupted (Something changed VBO2 while it shouldn't have been changed)
	//     --> VAO[1] wasn't properly set up - vertex attribute pointer needed to be set up seperately from VAOs[0] 
	// ****************************************************************************
	float vertices1[] = {
		// First Triangle
		-0.7f, -0.5f, 0.0f,
		-0.4f,  0.5f, 0.0f,
		-0.1f, -0.5f, 0.0f
	};	// The 'depth' of the triangle remains the same, making it look like it's 2D. (Depth 0)

	float vertices2[] = {
		// Second Triangle
		0.1f, -0.5f, 0.0f,
		0.4f,  0.5f, 0.0f,
		0.7f, -0.5f, 0.0f
	};

	// Generate 2 VBOs and VAOs
	unsigned int VBOs[2], VAOs[2];	// Wow this is smart. Store multiple VBOs and VAOs in arrays of them!
	glGenVertexArrays(2, VAOs);		// We can also generate multiple VAOs or buffers at the same time 
	glGenBuffers(2, VBOs);

	// First Triangle Setup
	glBindVertexArray(VAOs[0]);
	glBindBuffer(GL_ARRAY_BUFFER, VBOs[0]);
	glBufferData(GL_ARRAY_BUFFER, sizeof(vertices1), vertices1, GL_STATIC_DRAW);
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 3 * sizeof(float), (void*)0);
	glEnableVertexAttribArray(0);
	// glBindVertexArray(0);	// No need to unbind at all as we directly bind a different VAO the next few lines 

	// Second Triangle Setup
	glBindVertexArray(VAOs[1]);
	glBindBuffer(GL_ARRAY_BUFFER, VBOs[1]);
	glBufferData(GL_ARRAY_BUFFER, sizeof(vertices2), vertices2, GL_STATIC_DRAW);
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 3 * sizeof(float), (void*)0);	// We can also specify 0 as the vertex attribute's stride to let OpenGL figure it out bc it is tightly packed 
	glEnableVertexAttribArray(0);
	glBindVertexArray(0);		// Not really necessary as well, but beware of calls that could affect VAOs while this one is bound 

	// ---------------------------------------------------------------------
	// Setting GL Viewport
	// ---------------------------------------------------------------------
	// ** If OpenGL draws in less space of the window, the rest of the space can be used for GUI panels, Text overlay, toolbars, etc.
	// Normalized Device coordinates to Window Coordinates(Viewport)
	glViewport(0, 0, 800, 600);	// Param: Location of the lower left corner(first two) | Width and height of the RENDERING WINDOW in pixels

	// ---------------------------------------------------------------------
	// RENDER LOOP
	// ---------------------------------------------------------------------
	// Uncomment this call to draw in wireframe polygons
	// glPolygonMode(GL_FRONT_AND_BACK,		// Specifies the polygons the mode applies to (FRONT, BACK, FRONT_AND_BACK)
	// 	GL_LINE);				// Specifies how the polygons will be rasterized (POINT, LINE, FILL)

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

		// Draw first triangle
		// Activate Shader Program object 
		glUseProgram(firstShaderProgram);		// Every shader & rendering call after glUseProgram will now use this program object
		glBindVertexArray(VAOs[0]);
		glDrawArrays(GL_TRIANGLES, 0, 3);
		// Draw Second Triangle
		glUseProgram(secondShaderProgram);
		glBindVertexArray(VAOs[1]);
		glDrawArrays(GL_TRIANGLES, 0, 3);

		// GLFW: Check and Call EVENTS and SWAP the BUFFERS
		glfwSwapBuffers(window);	// Swap the color buffer(2D buffer) that is used to render during this render iteration and show it as output to the screen
		// ** Double Buffer
		//    Front buffer - Contains the final output image that is shown at the screen,
		//	  Back buffer  - Rendering commands draw to the back buffer
		//	As soon as ALL the rendering commands are finished, swap the back buffer to the front buffer so the image can be displayed without still being rendered to			
		glfwPollEvents();			// Checks if any events are triggered. Updates window state and calls corresponding functions (registered via callback methods)
	}

	// Optional: De-allocate all resources once they've outlived their purpose:
	glDeleteVertexArrays(2, VAOs);
	glDeleteBuffers(2, VBOs);
	glDeleteProgram(firstShaderProgram);
	glDeleteProgram(secondShaderProgram);

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