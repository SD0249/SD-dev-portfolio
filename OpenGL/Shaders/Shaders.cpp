// ---------------------------------------------------------------------
// Good to knows
// * Because OpenGL is in its core a C LIBRARY, it does not have native support for function overloading.
//   Wherever a function can be called with different types, OpenGL defines NEW functions for EACH TYPE required
// ---------------------------------------------------------------------

#include <glad/glad.h>
#include <GLFW/glfw3.h>

#include <iostream>
using namespace std;

// ** Helper function declaration **
void framebuffer_size_callback(GLFWwindow* window, int width, int height);
void processInput(GLFWwindow* window);

// Settings
const unsigned int SCR_WIDTH = 800;
const unsigned int SCR_HEIGHT = 600;

// ---------------------------------------------------------------------
// SHADERS - Little Programs that rest on the GPU / Run for each SPECIFIC section of the graphics pipeline
//			 Isolated programs(The only communication between them is via their inputs and outputs)
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// * Shaders are written in C-like language GLSL. 
//   GLSL is tailored for use with Graphics and contains useful features specifically targeted at vector and matrix manipulation.
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// [ Typical Shader structure ]
// #version version_number	  // Always begin with a version declaration.
// in type in_variable_name;  // A list of input and output variables, uniforms
// in type in_variable_name;
// 
// out type out_variable_name;
// 
// uniform type uniform_name;
// 
// void main()				  // Main Function
// {
//		// Process input(s) and do some weird graphics stuff
//		...
//		// Output processed stuff to output variable
//		out_variable_name = weird_stuff_we_processed;
// }
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// * VERTEX SHADER: Each input variable is also known as a 'Vertex Attribute'
//					There is a maximum number of vertex attributes we're allowed to declare limited by the hardware
//		* To define how the vertex data is organized, we specify the input variables with LOCATION metadata 
//		  so we can configure the vertex attributes on the CPU. --> layout (location = 0)
//		* Requires an extra layout specification for its inputs so we can LINK it with the VERTEX DATA
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// * FRAGMENT SHADER
//		* Requires a vec4 color output variable (final output color).
//			* If not specified, the color buffer output for those fragments will be undefined 
//			  (OpenGL will render them either black or white) 
// 
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// [ TYPES ] Default Basic Types we know from C: int, float, double, uint, bool
//		Newly added! --> Vector & Matrices
// * Vector: 2, 3 or 4 component container for any of the basic types.
//		* vecn: the default vector of n floats
//		* bvecn: a vector of n booleans
//		* ivecn: a vector of n integers
//		* uvecn: a vector of n unsigned integers
//		* dvecn: a vector of n double components
// * You can access its components by .x, .y, .z, and .w | rgba for colors | stpq for texture coordinates
// * Swizzling (Some interesting and flexible component selection) 
// vec2 someVec;
// vec4 differentVec = someVec.xyxx;
// vec3 anotherVec = differentVec.zyw;
// vec4 otherVec = someVec.xxxx + anotherVec.yxzy;
// * You can also pass vectors as arguments to different vector constructor calls
// vec2 vect = vec2(0.5, 0.7);
// vec4 result = vec4(vect, 0.0, 0.0);
// vec4 otherResult = vec4(result.xyz, 1.0);
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// [ Ins & Outs ] in & out keywords
// * Each shader can specify inputs and outputs using keywords and wherever 
//   an output variable matches with an input variable of the next shader 
//   stage they're passed along. 
// * VERTEX shader & FRAGMENT shader differ a bit (Check above)
// * Sending data from one shader to the other 
//	 --> Declare an OUTPUT in the sending shader and a similar INPUT in the receiving shader
//		 When types and the names are equal on both sides, OpenGL will link those variables together
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// const char* vertexShaderSource = "#version 330 core\n"
//								"layout (location = 0) in vec3 aPos;\n"	// Attribute position 0
//								"out vec4 vertexColor;\n"					// Specify a color output to the fragment shader
//								"void main()\n"
//								"{\n"
//								"	  gl_Position = vec4(aPos, 1.0);\n"
//								"	  vertexColor = vec4(0.5, 0.0, 0.0, 1.0);\n"	// Set the output variable to a dark-red color 
// 								"}\0";
//
// const char* fragmentShaderSource = "#version 330 core\n"
//								"out vec4 FragColor;\n"	
//								"in vec4 vertexColor;\n"				
//								"void main()\n"
//								"{\n"
//								"	  FragColor = vertexColor;\n"
// 								"}\0";
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// [ Uniforms ]
// * Another way to pass data from our 'application' on the CPU to the 'shaders' on the GPU 
// * Uniforms are GLOBAL - is unique per shader program object and can be 
//						   accessed from any shader at any stage in the shader program
//  * Whatever you set the uniform value to, uniforms will KEEP their values 
//	  until they're either RESET or UPDATED 
//  * If you declare one you must use it - 
//	  If it isn't used anywhere, the compiler will silently remove the variable from the compiled
//	  version which is the cause for several frustrating errors!
// ---------------------------------------------------------------------
const char* vertexShaderSource = "#version 330 core\n"
								"layout (location = 0) in vec3 aPos;\n"	// Attribute position 0
								"void main()\n"
								"{\n"
								"	  gl_Position = vec4(aPos, 1.0);\n"
								"}\0";

const char* fragmentShaderSource = "#version 330 core\n"
								"out vec4 FragColor;\n"	
								"uniform vec4 ourColor;\n"	// We set this variable in the OpenGL code.			
								"void main()\n"
								"{\n"
								"	  FragColor = ourColor;\n"
								"}\0";

int main() {
	// ---------------------------------------------------------------------
	// Setting up glfw
	// ---------------------------------------------------------------------
	glfwInit();
	glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
	glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
	glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);


	// ---------------------------------------------------------------------
	// Create a window
	// ---------------------------------------------------------------------
	GLFWwindow* window = glfwCreateWindow(SCR_WIDTH, SCR_HEIGHT, "Shaders", NULL, NULL);
	if (window == NULL) {
		cout << "Failed to create GLFW window" << endl;
		glfwTerminate();
		return -1;
	}
	glfwMakeContextCurrent(window);
	glfwSetFramebufferSizeCallback(window, framebuffer_size_callback);

	// Use GLFW to retrieve OpenGL functions & Use GLAD to create function pointers
	if (!gladLoadGLLoader((GLADloadproc)glfwGetProcAddress)) {
		cout << "Failed to initialize GLAD" << endl;
		return -1;
	}


	// ---------------------------------------------------------------------
	// Build & Compile Shader Program
	// ---------------------------------------------------------------------
	// Query Maximum number of vertex attributes allowed --> Current Device: 32
	/*int nrAttributes;
	glGetIntegerv(GL_MAX_VERTEX_ATTRIBS, &nrAttributes);
	cout << "Maximum number of vertex attributes supported: " << nrAttributes << endl;*/

	// Vertex Shader
	unsigned int vertexShader;
	vertexShader = glCreateShader(GL_VERTEX_SHADER);
	glShaderSource(vertexShader, 1, &vertexShaderSource, NULL);
	glCompileShader(vertexShader);

	int success;
	char infoLog[512];
	glGetShaderiv(vertexShader, GL_COMPILE_STATUS, &success);
	if (!success) {
		glGetShaderInfoLog(vertexShader, 512, NULL, infoLog);
		cout << "ERROR::SHADER::VERTEX::COMPILATION_FAILED\n" << infoLog << endl;
	}

	// Fragment Shader
	unsigned int fragmentShader;
	fragmentShader = glCreateShader(GL_FRAGMENT_SHADER);
	glShaderSource(fragmentShader, 1, &fragmentShaderSource, NULL);
	glCompileShader(fragmentShader);

	glGetShaderiv(fragmentShader, GL_COMPILE_STATUS, &success);
	if (!success) {
		glGetShaderInfoLog(fragmentShader, 512, NULL, infoLog);
		cout << "ERROR::SHADER::FRAGMENT::COMPILATION_FAILED\n" << infoLog << endl;
	}

	// Create Shader Program
	unsigned int shaderProgram;
	shaderProgram = glCreateProgram();
	glAttachShader(shaderProgram, vertexShader);
	glAttachShader(shaderProgram, fragmentShader);
	glLinkProgram(shaderProgram);

	glGetProgramiv(shaderProgram, GL_LINK_STATUS, &success);
	if (!success) {
		glGetProgramInfoLog(shaderProgram, 512, NULL, infoLog);
		cout << "ERROR::SHADER::PROGRAM::LINKING_FAILED\n" << infoLog << endl;
	}

	// Make sure to delete shader objects once linking them to the project
	glDeleteShader(vertexShader);
	glDeleteShader(fragmentShader);

	// ---------------------------------------------------------------------
	// Set up VERTEX data (and buffer(s)) and configure vertex attributes
	// ---------------------------------------------------------------------
	float vertices[] = {
		-0.5f, -0.5f, 0.0f,
		 0.5f, -0.5f, 0.0f,
		 0.0f,  0.5f, 0.0f
	};

	unsigned int VBO, VAO;
	glGenBuffers(1, &VBO);
	glGenVertexArrays(1, &VAO);

	glBindVertexArray(VAO);
	glBindBuffer(GL_ARRAY_BUFFER, VBO);
	glBufferData(GL_ARRAY_BUFFER, sizeof(vertices), vertices, GL_STATIC_DRAW);

	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 3 * sizeof(float), (void*)0);
	glEnableVertexAttribArray(0);
	glBindVertexArray(0);	// Unbinding VAO


	// ---------------------------------------------------------------------
	// RENDER LOOP
	// ---------------------------------------------------------------------
	while (!glfwWindowShouldClose(window)) {
		// GLFW: INPUT
		processInput(window);

		// Rendering Commands
		// Clear viewport
		glClearColor(0.2f, 0.3f, 0.3f, 1.0f);
		glClear(GL_COLOR_BUFFER_BIT);

		// BE SURE TO ACTIVATE THE SHADER before accessing its uniform val
		glUseProgram(shaderProgram);	// Use program

		// Use uniform value to gradually change the color of the triangle over time
		float timeValue = glfwGetTime();
		float greenValue = (sin(timeValue) / 2.0f) + 0.5f;
		int vertexColorLocation = glGetUniformLocation(shaderProgram, "ourColor");
		glUniform4f(vertexColorLocation, 0.0f, greenValue, 0.0f, 1.0f);	// Then update the value of uniform variable on the CURRENTLY ACTIVE shader program
		
		// Now render the triangle
		glBindVertexArray(VAO);
		glDrawArrays(GL_TRIANGLES, 0, 3);

		// GLFW: Check and call events and swap the buffers
		glfwSwapBuffers(window);
		glfwPollEvents();			// Checks if any events are triggered
	}

	// Optional: Deallocate all resources once they've all outlived their purpose:

	glfwTerminate();
	return 0;	// Main function terminates successfully
}

// ** Helper function definition **
// Resize the viewport with user's window resizing
void framebuffer_size_callback(GLFWwindow* window, int width, int height)
{
	glViewport(0, 0, width, height);
}

// Keep code input organized
// Process all input: Query GLFW whether relevant keys are pressed/released
void processInput(GLFWwindow* window)
{
	if (glfwGetKey(window, GLFW_KEY_ESCAPE) == GLFW_PRESS)
		glfwSetWindowShouldClose(window, true);
}