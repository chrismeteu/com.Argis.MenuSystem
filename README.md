# com.Argis.MenuSystem


How to use
1. Create Menu Manager
    a. Create a GameObject in the Hierarchy
    b. Add the MenuManager component
    c. Add back to Project folder and make a new varient
3. Create menu prefabs
    a. Create new script
    b. Replace the Monobehavior base with the Menu base class
    c. You must have at least one menu that uses the MainMenu base class
    d. Menus that are destroyed when closed should be self contained. This means loading and unloading all properties and fields
    e. You can use the 'Template Menu - Phone prefab' or use your own base to create all your menu prefabs from.
    f. Add your menu class component to the Menu's GameObject
    e. Add back to Project folder and make a new varient
4. Create a MenuList asset
    a. Right click in a project folder
    b. Create/Argis/Menus/Menu List
    c. Add all the menus you will ever use at runtime. You don't have to worry about memory because it's automatically handled.
    d. You can create different menu lists for different platforms
5. Add the menu list to their respective slots in the MenuManager component


To open a menu write the following in c#: MenuClassName.Open();

