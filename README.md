Sitecore - CheckPasswordExpiration
==================================

Summary
--------------

This module enforce the password expiration policy for sitecore shell users.

Usage
--------------
After package installation change the configuration "CheckPasswordExpiration.config",if needed.

for screenshoots,please check: https://github.com/alkoky/CheckPasswordExpiration/tree/master/Images  
  
Setup
--------------
Either:
* Install Sitecore package: 
  https://github.com/alkoky/CheckPasswordExpiration/blob/master/releases/Check-Password-Expiration-1.0.zip
			
Or:
1. Include this project in your Helix style solution
2. Update NuGet references to match target Sitecore version
3. Install sitecore package for data or sync unicorn

Notes
--------------
This was built and tested with Sitecore 8.1 update 2.

This package adds the following files:

     /bin/Common.Feature.CheckPasswordExpiration.dll
	 
     /sitecore/changepassword.aspx

     /App_Config/Include/Security/CheckPasswordExpiration.config

	
Credit
----------
Many thanks to Mike Reynolds: https://sitecorejunkie.com/2013/06/08/enforce-password-expiration-in-the-sitecore-cms/
