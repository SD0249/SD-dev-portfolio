#include <glad/glad.h>
#include <GLFW/glfw3.h>

#include <iostream>
using namespace std;

#include "Shader.h"

void framebuffer_size_callback(GLFWwindow* window, int width, int height);
void processInput(GLFWwindow* window);

// Settings
const unsigned int SCR_WIDTH = 800;
const unsigned int SCR_HEIGHT = 600;

// ------------------------------------------------------------------
// TEXTURES - A 2D image (even 1D and 3D textures exist) used to add detail to an object
// ------------------------------------------------------------------
// * Each VERTEX should have a 'TEXTURE COORDINATE' associated with them 
//	 that specifies what PART of the texture image to sample from. (Fragment interpolation handles the rest)
//		* Texture coordinates range from 0 to 1 in the x and y axis
// * Retrieving the texture color using texture coordinates is called 'SAMPLING'.
//		* Sampling has a loose interpretation and can be done in many different ways.
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// [ TEXTURE WRAPPING ] : Repeating Texture Images
//			* GL_REPEAT: The default behavior for textures. Repeats the texture image
//			* GL_MIRRORED_REPEAT: Same as GL_REPEAT but mirrors the image with each repeat
//			* GL_CLAMP_TO_EDGE: Clamps the coordinates between 0 and 1. The result is that higher coodinates becomes clamped to the edge,
//								resulting in a STRETCHED EDGE pattern
//			* GL_CLAMP_TO_BORDER: Coordinates OUTSIDE the range are now 
//								  given a USER-SPECIFIC BORDER COLOR
// * Each of the options can be set per coordinate axis (s, t, r equivalent to x, y, z)
//   with glTexParameter* function
//	 glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_MIRRORED_REPEAT);
//   glTexParameteri(GL_TEXTURE_2D,			// Texture Target
//					 GL_TEXTURE_WRAP_T,		// What option we want to set for which axis
//					 GL_MIRRORED_REPEAT);	// Pass in the texture wrapping mode we'd like
// * For GL_CLAMP_TO_BORDER  
//   float borderColor[] = { 1.0f, 1.0f, 0.0f, 1.0f };
//	 glTexParameterfv(GL_TEXTURE_2D, GL_TEXTURE_BORDER_COLOR, borderColor);
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// [ TEXTURE FILTERING ] Mapping Texture Coordinate to which Texture Pixel(Texel)
//		* GL_NEAREST (Nearest Neighbor or Point filtering) - Default:
//		  OpenGL selects the texel where its center is closest to the texture coordinate
//		* GL_LINEAR ((bi)linear filtering)
// ------------------------------------------------------------------

int main() {
	// GLFW: Initialize and configure
	glfwInit();
	glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
	glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
	glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);

	// GLFW window creation
	GLFWwindow* window = glfwCreateWindow(SCR_WIDTH, SCR_HEIGHT, "Textures", NULL, NULL);
	if (window == NULL) {
		cout << "Failed to create GLFW window" << endl;
		glfwTerminate();
		return -1;
	}
	glfwMakeContextCurrent(window);
	glfwSetFramebufferSizeCallback(window, framebuffer_size_callback);

	// Glad: Load all OpenGL function pointers
	if (!gladLoadGLLoader((GLADloadproc)glfwGetProcAddress)) {
		cout << "Failed to initialize GLAD" << endl;
		return -1;
	}

	// Build and Compile Shader program


	// Set up vertex data and buffer(s) and configure vertex attributes 


	// Texture stuff!


	// Render Loop
	while (!glfwWindowShouldClose(window)) {
		// INPUT
		processInput(window);

		// Render!!
		glClearColor(0.2f, 0.3f, 0.3f, 1.0f);
		glClear(GL_COLOR_BUFFER_BIT);



		// GLFW: Swap buffers and poll IO events
		glfwSwapBuffers(window);
		glfwPollEvents();
	}

	// Optional: De-allocate all resources


	glfwTerminate();
	return 0;
}

void processInput(GLFWwindow* window) {
	if (glfwGetKey(window, GLFW_KEY_ESCAPE) == GLFW_PRESS) {
		glfwSetWindowShouldClose(window, true);
	}
}

void framebuffer_size_callback(GLFWwindow* window, int width, int height) {
	glViewport(0, 0, width, height);
}


