# Installation Guide

## Requirements

Make sure, all of the following requirements are met:
- Unity 2019.4 or higher installed
- nodejs 14.16.1 or higher installed

##Setting Up in Unity

Clone the repository to the desired location on your machine.
A folder named _awt-unity_ should be created.

`git clone git@git.tu-berlin.de:alex94/awt-unity.git`

Use either Unity Hub or the Unity editor to open the project
(select the folder created by the git clone command). The first opening may take
a while since Unity has to import all assets and packages.

Inside the Unity editor open the _Project View_.
(ctrl + 5 or _Window_ -> _General_ -> _Project_)

Navigate to _Assets_ -> _Scenes_ and open the _Prototype_ scene.

##Setting Up the Web Server

Navigate to the _WebApp_ folder inside the _awt-unity_ folder. Open the _package.json_ file.
Change the start script (_scripts_ -> _start_) to

`node ./build/index.js -w -p 8080`.

Open a terminal or console window inside the _WebApp_ folder. Run the following commands in this particular order:

`npm install`

`npm run build`

##Starting the Web Server and the Application

Start the web server by executing the following command inside the _WebApp_ folder:

`npm run start`

To verify the web server is running, open the URL _localhost:8080_ in a browser.

Click the play button inside the Unity editor.

On the website at _localhost:8080_ click on _VideoPlayer Sample_.

Click the play button of the video player on the next site.

You can now control the capsule with WASD in the browser.

##Trouble Shooting

Make sure the web server and the application are **NOT** running.

In case the port 8080 is already occupied:

- inside the Unity editor, select the _RenderStreaming_ game object. In the inspector,
  change the port number under _Render Streaming_ -> _Signaling URL_.
- inside the package.json change the port number in the start script to the same number
  as in Unity.
  
  
The video in the browser stays black:

- inside the Unity editor, select the _RenderStreaming_ game object. In the inspector,
  change the _Signaling Type_ to _HttpSignaling_ and change the _Signaling URL_ from
  _ws://localhost:8080_ to _http://localhost:8080_.
- inside the package.json remove the _-w_ option from the start script.


