# N64noAAPatcher
Simple Windows app for patching any n64 ROM to disable AA

## Description
I made this little app just to automate tasks that I need to do for several hundred ROM-files. I'm not sure if it will be useful to anyone. However, if there are requests, I can improve the application by adding features and fixing it. 

## Features
 - Convert *.n64 and *.v64 to *.z64
 - Disable AA in ROM
 - Update checksum of ROM
 - Save result file

## Issues
 - Because all work in main thread, after click "Start" button UI no respond.
 - CLI-apps run just with timeout
 - Optimization needed
 - If some ROMs not in output path, then input ROM not compatible with noAA fix or something wrong with input ROM-file

## How to use
 - Run N64noAAPActher.exe
 - Add files or directory with files (subdirectories are allow)
 - Choose output path
 - Click "Start" button
 - Be patient ;)