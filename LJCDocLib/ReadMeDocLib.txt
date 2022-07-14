Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
ReadMeDocLib.txt

BuildDocLib.cmd - Builds the solution without starting Visual Studio. This
includes calling "UpdateDocLib.cmd".

ClearDocLib.cmd - Removes the compiled and generated code to make the solution
projects ready to be backed up.

CopyUpdateLinks.cmd - 

UpdateDocLib.cmd - Copies the referenced and runtime dependencies for the
solution projects.

UpdateLinks.cmd - Copies the project Link Pages to the CodeDoc project folders.
This file should be copied to the CodeDoc root folder and run from there.

ProjectFolder\LinkPages -
Each project can have one or more folders to hold link pages. There
should be a folder named LinkPages. There may also be other folders such
as Links, Diagrams, etc. to qualify the type of links.

LJCDocGen\bin\Debug\Links -
There must be an XML file in LJCDocGen\bin\Debug\Links with the name of the
project and a suffix of Links. For example: Project LJCDataAccess would require
a file named LJCDataAccessLinks.xml. This file identifies the links to be added
to the CodeDoc Project page. A link is created on the Project page for each
entry.

<?xml version="1.0"?>
<!-- Copyright (c) Lester J. Clark 2017-2019 - All Rights Reserved -->
<!-- LJCDataAccessLinks.xml -->
<Links xmlns:xsd="http://www.w3.org/2001/XMLSchema"
 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <Link>
    <Name>..\..\..\..\LJCDataAccess\LJCDataAccess\LinkPages\DataAccessLink.html</Name>
    <Text>Data Access Class Diagram</Text>
  </Link>
</Links>

