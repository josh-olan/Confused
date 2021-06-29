# DIRECTORY AND CODE ORGANIZATION

**Base**  
Contains class and function that creates an instance of the webdriver.

**Helpers** 
- DataGenerator => Generates random string of given length of param.
- UA => Contains functions to get elements from the dom, selecting from dropdowns and user actions; clicking and inputting text.

**Models**  
Contains models for objects e.g. Computer object.

**Pages** 
Contains the pages of the web application; following the POM approach.

**Tests** 
Contains the test class files.

On running the test(s), the driver object is created before each test in the 'Setup' function, the driver navigates to the homepage and maximises the window.
For 'Verify_User_Can_Add_A_Computer' test, a new computer is created, an assertion to check if a success message is displayed and checks if the computer is present in the table.
For 'Verify_User_Can_Delete_Computer_From_List', a random computer is chosen, the computer is clicked, deleted and an assertion to check the computer is not present.
For 'Verify_A_User_Can_Filter_By_Name', a random computer name is selected, entered in the searchbox and an assertion to check all computers displayed contain the search text.