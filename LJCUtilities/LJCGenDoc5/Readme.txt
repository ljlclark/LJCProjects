LJCUtilities\LJCGenDoc5.sln
This .NET solution was started in an attempt to mirror the .NET Framework
program CoreUtilities\LJCGenDoc2.sln.

LJCGenDoc2 was started to experiment with custom C# parsing to use as an
example and template for doing PHP parsing. It was also used to experiment with
the new class <Group> tag and method <ParentGroup> tag.

Both the LJCGenDoc2 and LJCGenDoc5 programs have been backed up and abandoned.

The original .NET Framework program, LJCGenDoc, functionality was:
1. Used the C# XML Documentation files for XML documentation values.
2. Used Assembly Reflection to obtain the Class, Method, Property and Field
   syntax.
3. Used a database to group items in the output HTML.

A New LJCGenDoc3 Program

The database editing interface was considered to clumsy for grouping so the new
tags will be experimented with as they appear in the C# XML Documentation files.

New tags TBD will be experimented with to eliminate the database Assembly
grouping.

The XML Documentation values will be experimented with to see if they can
provide the Syntax text.

An attempt will be made to simplify the new program.
1. The transformation of the XML Documentation file to the DocData XML files
   will be separate modules or classes for each target template.
2. The combining of the GenData XML file and template will be handled with the
   separate module LJCGenText5.

An attempt will be made to have the LJCGenText5/LJCGenTextEdit5 program handle
the SubSection data.


 