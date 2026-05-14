#ifndef SHADER_H	// #ifndef & #endif can be substituted with #pragmaonce
#define SHADER_H

#include <glad/glad.h>	// Include glad to get all the required OpenGL headers

#include <string>
#include <fstream>
#include <sstream>		// A standard library header that allows you to treat strings as streams
#include <iostream>

/// <summary>
/// Reads shaders from disk, compiles and links them, and checks for errors.
/// </summary>
class Shader
{
public:
	unsigned int ID;	// The program ID

	Shader(const char* vertexPath, const char* fragmentPath) {	// Constructor reads and builds the shader
		// ---------------------------------------------------------------------
		// 1. Retrieve the vertex/fragment source code from filePath
		// ---------------------------------------------------------------------
		std::string vertexCode;
		std::string fragmentCode;
		std::ifstream vShaderFile;
		std::ifstream fShaderFile;

		// Ensure ifstream objects can throw exceptions (Enables exception-based error handling / You could also check whether the file is null after opening)
		vShaderFile.exceptions	  // exception() functions tells the stream which ERROR states should trigger a THROWN exception (std::ios_base::failure)
		(std::ifstream::failbit   // Set when a logical I/O operation fails
			| std::ifstream::badbit); // Set when a serious low-level I/O error occurs
		fShaderFile.exceptions(std::ifstream::failbit | std::ifstream::badbit);

		// Try & Catch
		try
		{
			// Open files
			vShaderFile.open(vertexPath);
			fShaderFile.open(fragmentPath);
			std::stringstream vShaderStream, fShaderStream;

			// Read file's buffer contents into streams
			vShaderStream << vShaderFile.rdbuf();
			fShaderStream << fShaderFile.rdbuf();

			// Close file handlers
			// * If we want to open another file using vShaderFile or fShaderFile,
			//   file.clear() is needed to reset the flags (EOF or failbit, etc)
			vShaderFile.close();
			fShaderFile.close();

			// Convert stream into string
			vertexCode = vShaderStream.str();
			fragmentCode = fShaderStream.str();
		}
		catch (std::ifstream::failure e)
		{
			std::cout << "ERROR::SHADER::FILE_NOT_SUCCESSFULLY_READ" << std::endl;
		}

		// Store read source code to a constant string variable 
		// (to ensure that it won't be modified + Change it to c string)
		const char* vShaderCode = vertexCode.c_str();
		const char* fShaderCode = fragmentCode.c_str();

		// ---------------------------------------------------------------------
		// 2. Compile Shaders
		// ---------------------------------------------------------------------
		unsigned int vertex, fragment;

		// Vertex shader
		vertex = glCreateShader(GL_VERTEX_SHADER);
		glShaderSource(vertex, 1, &vShaderCode, NULL);
		glCompileShader(vertex);
		checkCompileErrors(vertex, "VERTEX");

		// Fragment shader
		fragment = glCreateShader(GL_FRAGMENT_SHADER);
		glShaderSource(fragment, 1, &fShaderCode, NULL);
		glCompileShader(fragment);
		checkCompileErrors(fragment, "FRAGMENT");

		// Shader program
		ID = glCreateProgram();
		glAttachShader(ID, vertex);
		glAttachShader(ID, fragment);
		glLinkProgram(ID);
		checkCompileErrors(ID, "PROGRAM");

		// Delete the shaders as they're linked into our program now and no longer necessary
		glDeleteShader(vertex);
		glDeleteShader(fragment);
	}

	void use() {	// Use/Activate the shader
		glUseProgram(ID);
	}

	// Utility uniform functions - Query a uniform location and set its value
	void setBool(const std::string& name, bool value) const {
		glUniform1i(glGetUniformLocation(ID, name.c_str()), (int)value);
	}

	void setInt(const std::string& name, int value) const {
		glUniform1i(glGetUniformLocation(ID, name.c_str()), value);
	}

	void setFloat(const std::string& name, float value) const {
		glUniform1f(glGetUniformLocation(ID, name.c_str()), value);
	}

private:
	// Utility Function for checking shader compilation/linking errors
	void checkCompileErrors(unsigned int shader, std::string type) {
		int success;
		char infoLog[1024];
		if (type != "PROGRAM")
		{
			glGetShaderiv(shader, GL_COMPILE_STATUS, &success);
			if (!success) {
				glGetShaderInfoLog(shader, 1024, NULL, infoLog);
				std::cout << "ERROR::SHADER_COMPILATION_ERROR of type: " << type << "\n" << infoLog << "\n -- --------------------------------------------------- -- " << std::endl;
			}
		}
		else
		{
			glGetProgramiv(shader, GL_LINK_STATUS, &success);
			if (!success) {
				glGetProgramInfoLog(shader, 1024, NULL, infoLog);
				std::cout << "ERROR::PROGRAM_LINKING_ERROR of type: " << type << "\n" << infoLog << "\n -- --------------------------------------------------- -- " << std::endl;
			}
		}
	}
};

#endif

// * Resource
// * How to read file stream data into string C++
// https://www.fromdev.com/2025/05/how-to-read-file-stream-data-to-string-in-c-2025-guide.html