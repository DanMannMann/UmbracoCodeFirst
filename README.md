# Code-First for Umbraco
[![Build status](https://danmann.visualstudio.com/UmbracoCodeFirst/_apis/build/status/UmbracoCodeFirst)](https://danmann.visualstudio.com/UmbracoCodeFirst/_build/latest?definitionId=1) (master branch)

[![Build status](https://danmann.vsrm.visualstudio.com/_apis/public/Release/badge/8908b2fb-3c5c-49e0-99d2-273814a16a86/1/1)](https://danmann.vsrm.visualstudio.com/_apis/public/Release/badge/8908b2fb-3c5c-49e0-99d2-273814a16a86/1/1) (Nuget package)

[![Build status](https://danmann.vsrm.visualstudio.com/_apis/public/Release/badge/8908b2fb-3c5c-49e0-99d2-273814a16a86/1/2)](https://danmann.vsrm.visualstudio.com/_apis/public/Release/badge/8908b2fb-3c5c-49e0-99d2-273814a16a86/1/2) (Docs site)

Code-First for Umbraco allows Umbraco types (document types, data types etc) to be specified in code and automatically
synchronised each time the site is started. The defined classes can also be used as strongly-typed models in views.

## Installing

Install-Package Marsman.UmbracoCodeFirst

## Documentation

See the [Documentation and Walkthrough](http://codefirst.marsman.co.uk/)

## Running the QuizDemo project

On first run the Umbraco install page will show. Enter the required info then choose customise, select your preferred 
database and choose "no starter kit". The site will then start, with Code-First having built out all the types and 
added the seed content.
