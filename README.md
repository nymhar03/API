ASP.NET WEB API
*Entity Framework Code First Development

*Using Asyncronous API methods

*Token based API to prevent multiple sign on

*Triple-DES encryption/decrption on password

STEPS TO RUN THE APPLICATION
1. Open the solution/project in VS 2019
2. Build the project to restore all the missing dependencies/libraries
3. Create/Migrate Database

	*Setup your SQL Connection string in Web.config file
	
	*Go to Package Manager Console
	
	*Select your Project.Data
	
	*PM> Add-Migration Initial
	
	*PM> Update-Database

4. Run Tests methods using AAA procedure (Arrange, Act and Assert)

	*CreateUserWithNoUsername
	
	*CreateUserWithNoPassword
	
	*CreateUserWithDifferentPasswords
	
	*CreateUserWithCorrectInfo
	
	*AuthenticateUserWithNoUsername
	
	*AuthenticateUserWithNoPassword
	
	*AuthenticateUserWithCorrectInfo

	Needs a User Id and Token. These serves as the validation that a user is authenticated before using API methods
	You will get those values by Loging-in from Home Page. Run the application first to perform login
	
	*CreateAccount
	
	*GetAccounts
	
	*CreatePayment
	
	*GetPaymentsByAccountNo

5. How to consume APIs

	*I created a generic class JSONViewModel.cs as a return value for each API methods. Below are the fields
	
	-ResponseMessage //Message information about the result of the api if Error/Success
	
	-StatusCode      //You can create/asign your own code. I set the success result to 100
	
	-Data			 //Optional. You can pass an object if needed

	*I created BaseController.cs wherein I added additional methods for Validations of User Id & token and auto generation of token
	
	*This base class is inherited to all API Controllers
	
	*See UsersController.cs, AccountsController.cs and PaymentsController.cs for the implementation
	
	*See Index.cshtml and Services.js on how to call API via ajax
	
	*To test API in the application, Build and Run Project.WebApi
	
		-Login >>Enter Username and Password (e.g. mark,12345 as sample in Test script "CreateUserWithCorrectInfo"
		
		-Create Account >>Used the user id and token from Login
		
		-Get Account >>Used the user id and token from Login. Enter your Account No.
		
		-Update Account >>Used the user id and token from Login. Enter your registered Account No.and other details that you want to update
		
		-Create Payment >>Used the user id and token from Login. Enter your registered Account No., Amount and Remarks
		
		-Get Payments by Account No >>Used the user id and token from Login. Enter your registered Account No.
	
	*You can also test API using POSTMAN or other api testing tools




    
