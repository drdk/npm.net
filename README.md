npm.net
=======
Npm.Net is a .NET library that implements a C# wrapper for common node npm commands.

To learn more about node npm see http://npmjs.org/.

The library includes 2 classes for interacting with node npm commands.
  - NpmApi has methods that closely mirror the npm commands.
  - NpmPackageManager is a slightly higher level abstraction that is useful when dealing with only immediate children.

Building
========
Visual Studio 2010 was used to create the solution and project files.
The solution includes unit tests that run within Visual Studio 2010. The tests use mock data and a mock client.
The solution also includes a sample application that uses the API to do some simple tasks against the actual repository.

Installation
============
A version of node and npm must be installed to use this wrapper.

Configuration
=============
The API can be configured from the calling application by providing the working directory path.
The API may also be configured with the following:
  - path to the location of node.exe
  - repository URL
  - http_proxy
  - https_proxy
  - timeout

Implementation
==============
The wrapper runs the npm client code to execute the actual commands. The output of the npm client is parsed to produce the C# objects that are returned.
If the command fails, a NpmException is thrown. The exception contains data obtained by parsing the npm error output.

