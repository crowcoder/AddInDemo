# AddInDemo
Demo project for a Visual Studio extension with a user interface demonstrating taking input from the user to dynamically generate code.

### THE SETUP
 1. Make sure the **BOSCCvsix** project is selected as the startup project.
 2. Edit the **BOSCCvsix** project debug settings to point to your install of visual studio, and provide the start argument that will start an "experimental" version of VS when you run.
 ![Debug Settings](https://contrivedxample.files.wordpress.com/2017/03/vsixdebugsettings.png)

### Run

When you run the project, another instance of Visual Studio will open. You will need to open a solution or create a new one. Once your solution is open, `Add New Item` and select your new template "BOSCC :

![BOSCCTemplate](https://contrivedxample.files.wordpress.com/2017/03/boscctemplate.png)

You will immediately see a window prompting you for a database server, database and table name. You must have access to an instance of MSSSQL Server to continue. A sample sql database project is included in the solution, complete with seed script, for convenience:

![BOSCC UI](https://contrivedxample.files.wordpress.com/2017/03/bosccprompt.png)

### Results
After you fill out the form and click "GO", you will have a new class file in your project with a property for each column in the table you selected. 

    using System;

    namespace ClassLibrary
    {
        class FruitGrowsOnModel
        {
            public int FruitGrowsOnID { get; set; }
            public string FruitGrowsOnName { get; set; }
        }
    }

### Take Aways

Creating a basic, static template in Visual Studio is simple. From the File menu, just export a file as Template and you are done. All of the work comes in when you must make your template dynamic and based on user input. 
The main points you need to know are:

1. Create 3 projects
   1. C# Item Template
   2. VSIX Project
   3. Regular class library
2. In the vsixmanifest of the VSIX project, add an Asset pointing to the C# Item Template project.
3. The class library project must have a class that implements IWizard. In our scenario this class is responsible for showing a WFP form, but it could be any means of receiving user input.
4. The class library assembly must be installed in the GAC - `gacutil -i your_assembly.dll`. This means you must sign the assembly. For demonstration purposes you can just create a new .snk file from the Signing tab of the project properties.
5. In the C# Item Template project, you must add a WizardExtension section to the .vstemplate file that references the assembly and class that implements IWizard.

### Installing

If you want to give your extension to other developers you can simply give them the .vsix file and the assembly that contains the IWizard. They will gac install the assembly and just double-click the .vsix to install it.

However, if you want to share your extension more broadly, like in the Visual Studio Extension Gallery for instance, you will need to sign your assembly with a real code signing certificate and create a real installer that does the extension and GAC work in one step.
