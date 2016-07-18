[comment]: # (Good markdown editor, that actually looks like github: https://jbt.github.io/markdown-editor)

# CK2Editor XML Format File Specification
  
  
### Overview
CK2Editor reads CK2 files based on pre-defined format files, written in xml.  
If the format file is missing information about parts of the file, the program will try its best to guess the missing information, but is prone to fail in some circumstances and cannot rely on this ability alone.  

The full format specification can be found in this document.

### Terminology
I realize there might be confusion between the different terms used in this document, so here is a clarification:  
+ "The file" / "The save file" - the CK2txt file that is being read. Not to be confused with "the format file"!  
+ "The format file" - the XML file that describes the format of the file  
+ "The (base) program" - The non-graphical part of CK2Editor  
+ "The GUI" - The graphical editing program  
+ "Value" - An identifier-value pair in the file, like `type=66`  
+ "Section" - A section of the file delimited by curly brakcets (`{ }`), also with an identifier (`player={...}`)  
+ "Entry" - A value or section  
+ "Element" - an XML element in the format file  
+ "Attribute" - an XML attribute of an element  

This terminology is also applied in the documentation and inline code comments.

### Top Structure
The format file must contain a root element called `File`. This represents the entire file after the CK2txt header, and all entries should be decendants of this element.  
before the `File` element, an XML declaration is recommended.  

### References
Some attributes allow references or value references. These are references to other entries that are related to the current entry. Normal references reference an entry itself, and value references reference related strings.

##### Symbols
references can contain symbols, which are strings related to the entries the reference path started at or is at now. Below is a list of valid symbols:  
+ `[THISNAME]` - The internal name of the entry the path started at  
+ `[THISVNAME]` - The user-friendly ("visual") name of the entry the path started at  
+ `[THISVALUE]` - The value of the entry the path started at

##### Normal References
The syntax of normal references is as follows:  
+ The path starts from the current entry, unless it starts with a `!` in which case it jumps to the the root (the `file` element in the format file)
+ A forward slash (`/`) seperates entries in the path
+ An entry name takes the path into that entry
+ two dots (`..`) take the path to the current entry's parent

##### Value References
Value references are based on normal references, but evaluate to strings. The syntax of value references is as follows:  
+ The reference is delimited by the sequences `[!` and `!]`  
+ The reference contains a notmal reference  
+ At the end of the normal references, there will be a colon (':') followed by one of the following special symbols:  
  * `[NAME]` - The internal name of the current entry  
  * `[VNAME]` - The user-friendly ("visual") name of the current entry  
  * `[VALUE]` - The value of the current entry  
  
Multiple and nested value references are supported.

##### Example
For example, the following value reference, as the `name` attribute of a value conataining a character id:
>`[!!/character/[THISVALUE]:[VNAME]!]`  

will evaluate to the user-friendly name of the character, because:  
1. `[!..!]` - value reference  
2. `!` - start from the root  
3. `/character/[THISVALUE]` - go to the section named the same as the value of this entry in the `character` section  
4. `:[VNAME]` - get the user-friendly name of that section

### General Entry syntax
Elements in the format file should correspond to entries in the file. The name of the element should be same as the identifier of the entry.  
In addition, the following attributes can be present:  
+ `name` - Required - The user-friendly name of the entry, that will be displayed to the user in the GUI. Can use value references.  
+ `multiple` - Optional - specifies that the element represents multiple entries in the file, that share a common structure. Can have the following values:  
  * `same` - All the entries share the same name  
  * `blank` - All the entries have no identifiers  
  * `number` - All the entries have different numbers as their identifiers  
  * `different` - All the entries have different, unrelated names. Causes all the entries in the current section to be associated with the element  
    
+ `grouper-name` - Required on elements that specify the `multiple` attribute - The user-friendly name of the grouper section which will be used in the GUI to group all entries which share this element. The value of this attribute should be put in parentheses so the user can distinguish it from real sections, but this is not enforced.  
  
  The name of an element with a multiple attribute of anything but `same` will be ignored  

### Value Entry Syntax
Elements describing value entries should have no children.
Value entries may have the additional attributes:  
+ `type` - Recommended - the type of this value. Can have the following values:  
  * `string` - The value is delimited by quotation marks ('"'). This is the only type the matters for the base program.  
  * `number` - The value is a number (can be fractional)  
  * `date` - The value is a date, in a year.month.day format  
  * `misc` - the value is of a miscellanous type. If none of the above are specified, this is assumed  
+ `link` - Optional - A link containing a reference to another entry, which is related to this one  

### Section Entry Syntax
Elements describing section entries should have children.
Sections may have these additional attributes:  
+ `series` - Optional - Specifies that this section should be formatted in the file as a series of values. Can have the following values:  
  * `normal` - The values are on their own line, with the closing brace of this section on that same line.  
    Example (from a `dynasty` section):  
    ```
    	    			data=
    	    			{
        0 1 0 4 10 13 0 				}
    ```
    Note that the indent is *after* the values. This is how it appears in original saves, and the program replicates this behaviour.  
  * `compact` - The entire section is on the same line as its identifier.  
  Example (from a `character` section):  
    ```
    traits={18 101 88 85 84 }
    ```
     

