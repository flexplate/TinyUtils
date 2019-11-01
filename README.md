# TinyUtils
A collection of tiny Windows utilities.

## Bitness
Command line program that reads the PE and COFF headers of an assembly to find the machine type it was compiled for.

#### Usage
`Bitness [path to assembly]` will output the machine type.

## MouseSpeed
Get and/or set Windows' mouse speed from the command line.

#### Usage
`MouseSpeed` - enter interactive mode. Tells you the current mouse speed and prompts for a new one.

`MouseSpeed [speed]` - set the mouse speed to `[speed]` directly.

## Measure
Windows Forms application that lets you measure distances on screen.