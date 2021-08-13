# Installation Guide

## Requirements

Make sure, all of the following requirements are met:
- Unity 2019.4 or higher installed
- nodejs 14.16.1 or higher installed

## Setting Up in Unity

Clone the repository to the desired location on your machine.
A folder named _awt-unity_ should be created.

`git clone git@git.tu-berlin.de:alex94/awt-unity.git`

Use either Unity Hub or the Unity editor to open the project
(select the folder created by the git clone command). The first opening may take
a while since Unity has to import all assets and packages.

Inside the Unity editor open the _Project View_.
(ctrl + 5 or _Window_ -> _General_ -> _Project_)

Navigate to _Assets_ -> _Scenes_ and open the _Prototype_ scene.

## HTTPS
If you plan to use the gyroscope and https, you don't have to change anything.

## HTTP
If you want to use http instead of https, you have to configure the project as follows.
Change the _Signaling URL_ of the _Render Streaming_ script of the _RenderStreaming_ game
object to _http://localhost:8080_

## Setting Up the Web Server

### HTTPS
The default configuration is for https. You don't have to change anything on the web server for https to work.

Open a terminal or console window inside the _WebApp_ folder. Run the following commands in this particular order:

`npm install`

`npm run build`

### HTTP
**NOTE: The gyroscope does not work without HTTPS**

Navigate to the _WebApp_ folder inside the _awt-unity_ folder. Open the _package.json_ file.
Change the start script (_scripts_ -> _start_) to

`node ./build/index.js -p 8080`.

Open a terminal or console window inside the _WebApp_ folder. Run the following commands in this particular order:

`npm install`

`npm run build`

## Setting up Batchmode (Windows only)
To run the application without the Unity editor and any visible game window, you have to build the application
using the Unity standalone player.

Inside the editor, click on _File_ and _Build Settings..._. Select your target platform click on _Build_ and
select a folder, where you want to save your application.

## Starting the Web Server and the Application

### Web server
Start the web server by executing the following command inside the _WebApp_ folder:

`npm run start`

To verify the web server is running, open the URL _localhost:8080_ in a browser. Add _https://_ in front of the URL
in case you are using **HTTPS**.

### Application
You can change the streamed resoultion by editing the _Streaming Size_ of the _Adjustable Camera Streamer_ script
on the _PlayerCamera_ game object. To start the application from inside the editor,
click the play button inside the Unity editor.

Alternatively, you can run the _.exe_, you created when setting up the batchmode. For that, navigate to the
folder of the _.exe_ and execute the following command:

`.\Prototype.exe -batchmode`

The application will run in the background. To close it down, simply use the Task Manager to kill the process.

To change the resolution when starting the application in batchmode you can use the following command:

`.\Prototype.exe -batchmode -streamingWidth=YOUR_WIDTH -streamingHeight=YOUR_HEIGHT`

The default width is 1280 and the default height is 720.
## Using the Application
On the website at _localhost:8080_ click on _Open Video Player_.

Click the play button of the video player on the next site.

You can now
- control the character with WASD
- move the camera while holding down the right mouse button
- control the character with the left stick of a gamepad
- control the camera with the right stick of a gamepad
- control the camera with the gyroscope (either by tilting the phone or using developer tools in certain browsers)
- control the character by touching the upper, lower, left or right part of the streamed video

Multiple client devices can connect to the same video stream. However, only the first client can control
the character and the camera. If the first client disconnects, the first newly connected client receives control.

## Trouble Shooting

Make sure the web server and the application are **NOT** running.

In case the port 8080 is already occupied:

- inside the Unity editor, select the _RenderStreaming_ game object. In the inspector,
  change the port number under _Render Streaming_ -> _Signaling URL_.
- inside _WebApp_ folder open the _package.json_ and change the port number in the _start_ script to the same number
  as in Unity.
  
Problems with https and the certificates:

- replace the content of _client-1.local.crt_ and _client-1.local.key_ inside the _WebApp_ folder with your own
certificate and private key.
- alternatively, you can change the location of the certificate and the key by editing the _package.json_ inside
the _WebApp_ folder. Inside the _start_ script, replace _client-1.local.key_ and _client-1.local.crt_ with the paths
to your key/certificate.

## Known Issues
When using the gyroscope, the camera spins rapidly around on a certain angles. This is due to the so called
[gimbal lock](https://https://en.wikipedia.org/wiki/Gimbal_lock).

In batchmode, the mouse and the right stick of the gamepad are not working. This is most likely caused by
weird interactions with the batchmode and the new Unity Input System.