>After many great years I've moved on from Felinesoft. ~~This repository has been forked to https://github.com/sacredgeometry/UmbracoCodeFirst which is now where all active maintenance will happen.~~

Note: I have also also moved on to greener pastures (after slightly more years, it is a competition btw), so maintenence is probably halted unless I can convince my new company to adopt this library.

# Code-First for Umbraco

Code-First for Umbraco allows Umbraco types (document types, data types etc) to be specified in code and automatically
synchronised each time the site is started. The defined classes can also be used as strongly-typed models in views.

## Installing

Install-Package Felinesoft.UmbracoCodeFirst

## Documentation

See the [Documentation and Walkthrough](http://codefirst.marsman.co.uk/)

## Running the QuizDemo project

On first run the Umbraco install page will show. Enter the required info then choose customise, select your preferred 
database and choose "no starter kit". The site will then start, with Code-First having built out all the types and 
added the seed content.
