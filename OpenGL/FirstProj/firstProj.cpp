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
#include <glad/glad.h>
#include <GLFW/glfw3.h>

#include <iostream>
using namespace std;

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
	GLFWwindow* window = glfwCreateWindow(800, 600, "LearnOpenGL", NULL, NULL);	// Param: Width, Height, Name, Ignore
	if (window == NULL) {
		cout << "Failed to create GLFW window" << endl;
		glfwTerminate();	// Destroys all remaining windows and cursors and frees any other resource
		return -1;
	}
	glfwMakeContextCurrent(window);		// Make the context of this window the main context on the current thread

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
	// Viewport
	// ---------------------------------------------------------------------
	// Before we start rendering - Tell OpenGL the size of the rendering window!!
	// How we want to display data and coordinates with respect to the window\
	// "ONLY DRAW INSIDE THIS RECTANGULAR REGION OF THE WINDOW"
	
	// Normalized Device coordinates to Window Coordinates(Viewport)
	glViewport(0, 0, 800, 600);	// Param: Location of the lower left corner(first two) | Width and height of the RENDERING WINDOW in pixels




	return 0; // main function is terminated
}