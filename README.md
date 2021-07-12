# yousign-api-client
C# implementation of Trakx' YouSign Api client

## Links

  - YouSign WebSite: https://yousign.com/

  - Web App Staging: https://staging-app.yousign.com
  - Web App Production: https://webapp.yousign.com

  - Api Base Staging: https://staging-api.yousign.com
  - Api Base Production: https://api.yousign.com

  - API Documentation: https://dev.yousign.com/
  - Sandbox Sign up: https://yousign.com/developer

## Creating your local .env file
In order to be able to run some integration tests, you should create a `.env` file in the `src` folder with the following variables:
```secretsEnvVariables
YouSignApiConfiguration__ApiKey=********
```

## Create you test account at yousign.com

 1) Navigate to "https://yousign.com/";
 2) Click on "Start free trial" button;
 3) Fill up all the fields and click on "Start your free trial now";
 4) Log into the Web App Staging;
 5) Click on "Admin" Menu and select "API Keys";
 6) Click on "Create An Api Key";
 7) You need to use this key in your .env file - variable "YouSignApiConfiguration__ApiKey".

## How to sign a document

 1) Execute the method "IFilesClient.FilesAsync(..)";
 2) Execute the method "IProceduresClient.ProceduresAsync(..)";
 3) The reponse of the second method will contain a member id;
 4) Add the iframe below at your website and pass the member id as follows:
`
<iframe src="https://staging-app.yousign.com/procedure/sign?members=/members/676e24cc-a396-4854-b798-371768f433fa"></iframe>
`
**Note:** To see one example of this follow, access the unit test "ProceduresClientTests.ProceduresClientTest_should_create_a_new_memberid_to_be_signed(..)"