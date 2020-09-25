# ImageContainer
This is a simple tool used to hide a file in a image. This approach was learnt from one of my professor who taught me Cpp during my undergrade days. He combined the requirement document of an assignment with an artwork and published them to us as a quizz. It worths 10% marks.

# Basic Idea
As we know, the color stored in the computer is seperated into three passes, that's `R`, `G` and `B`. Each pass uses `8 bits` to represent its strength. So, if we make some change to the `lowest 2 bits` of that data, only a very slight difference will be reflected on the result, which is usually invisible to human beings. A single pixel can hold 6 bits (2 bits from each pass), as a picture with the resolution of `1920 x 1080` can provide around `1.48 MB` capcity.

# How to use
1. Choose a picture. The max volume will be shown at the bottom.
2. If you want to hide a file into the picture, click the button at the top-right to choose a file.
3. Click `Combime!` to combine them. Set the directory and filename in the appearing dialog and wait until it finishes.
4. If you want to get the file hidden in the picture, click `Seperate!` and set the place where you want to save to.

# Limits
1. Only those formats which uses `lossless compression algorithm` can be used as the output, such as BMP and PNG. However, pictures in these formats are usually quite big in size.
2. Formats of input pictures are limited by `.Net Framework`. But I think they are plentiful enough.
