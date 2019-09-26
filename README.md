# Time Zoner
This repository provides an example of creating and integrating small-scale enterprise applications with a service-oriented architecture.

## Overview :eyes:
A **SOAP-based** and a **RESTful** web services were developed to deliver resources, valuable for knowing the UTC time :watch: of any specified country in the world :globe_with_meridians:. The services were deployed on a local server.Additionally, a client application was created for consuming the web services :pager:. 

For data persistence we are using a *csv* file with time offsets of each country. On the client side beside accessing our services we are contacting an additional web service from public repository to request the current GMT time. 

---

### Technical specifications :wrench:
We've implemented *SOAP* using a WCF application ([TimeZoner](https://github.com/elit0451/TimeZoner/tree/master/TimeZoner)) in C :hash: and for *REST* we used .NET CORE Web API ([TimeZonerRest](https://github.com/elit0451/TimeZoner/tree/master/TimeZonerRest)) that will allow us to easily containerize and deploy the application. </br>
The client application can be found in [TimeZonerClient](https://github.com/elit0451/TimeZoner/tree/master/TimeZonerClient) Class library.

</br>

## Getting Started
* Clone the project using the  following command **or** download the repository zip file </br>
`git clone https://github.com/elit0451/TimeZoner.git`
* Open the solution in Visual Studio :crystal_ball:
* Right click the *TimeZonerRest* project and execute as **Debug** (start a new instance).
* You would need to run the Client application next, so make sure you set it as a **default Start up project** and run it. 
* The WCF will start automatically.

## Functionality

</br>

___
> #### Assignment made by:   
`David Alves 👨🏻‍💻 ` :octocat: [Github](https://github.com/davi7725) <br />
`Elitsa Marinovska 👩🏻‍💻 ` :octocat: [Github](https://github.com/elit0451) <br />
> Attending "System Integration" course of Software Development bachelor's degree
