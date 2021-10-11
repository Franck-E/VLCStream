# VLCStream
Try changing in view / mediaoptions --avcodec-hw=none to avcodec-hw=d3d11va

Create a new youtube.luac file and replace existing in \packages\VideoLAN.LibVLC.Windows.3.0.16\build\x64\lua\playlist and x86,
with content from: 
https://github.com/videolan/vlc/blob/master/share/lua/playlist/youtube.lua
