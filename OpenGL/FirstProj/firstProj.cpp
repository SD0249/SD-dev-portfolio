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
	// ---------------------------------------------------------------------
	// Vertex shader

	// ---------------------------------------------------------------------
	// Set up VERTEX data (and buffer(s)) and configure vertex attributes
	// NOTE: All coordinates within normalized device coordinates range 
	//		 (-1.0 - 1.0) will end up visible on your screen
	// ---------------------------------------------------------------------
	float vertices[] = {
		-0.5f, -0.5f, 0.0f,
		 0.5f, -0.5f, 0.0f,
		 0.0f,  0.5f, 0.0f
	};	// The 'depth' of the triangle remains the same, making it look like it's 2D. 

	// Generate a vertext buffer object (VBO) with a buffer ID
	unsigned int VBO;
	glGenBuffers(1, &VBO);

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


	// ---------------------------------------------------------------------
	// Viewport
	// ---------------------------------------------------------------------
	// Before we start rendering - Tell OpenGL the size of the rendering window!!
	// How we want to display data and coordinates with respect to the window\
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


	


	// RENDER LOOP
	while (!glfwWindowShouldClose(window)) 
	{
		// INPUT
		// Check for specific key presses and react accordingly every frame
		processInput(window);


		// RENDERING COMMANDS HERE
		// Clear screen
		glClearColor(0.2f, 0.3f, 0.3f, 1.0f);	// State-Setting Function
		glClear(GL_COLOR_BUFFER_BIT);			// Pass in buffer bits to specify which buffer we would like to clear
		// State-Using function (ex. Uses current state to retrieve the clearing color from)

		
		// Check and Call EVENTS and SWAP the BUFFERS
		glfwSwapBuffers(window);	// Swap the color buffer(2D buffer) that is used to render during this render iteration and show it as output to the screen
									// ** Double Buffer
									//    Front buffer - Contains the final output image that is shown at the screen,
									//	  Back buffer  - Rendering commands draw to the back buffer
									//	As soon as ALL the rendering commands are finished, swap the back buffer to the front buffer so the image can be displayed without still being rendered to			
		glfwPollEvents();			// Checks if any events are triggered. Updates window state and calls corresponding functions (registered via callback methods)
	}

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