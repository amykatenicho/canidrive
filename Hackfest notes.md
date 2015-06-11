#hackfest notes


## stuff we learned
* The speech recognition is pretty good. Is it learning? Hard to get it not to recognise the phrase

## Tech used
* ASP.NET 5 (Beta4)
* Azure App Service Web Apps
* Azure Application Insights
* Project Oxford Speech to Text
* Project Oxford LUIS


## outline

```
                                                                                        
                                                                              
                                                                             
                                                                             
     +----------------------+                   +----------------------+     
     |                      |         4         |                      |     
     |                      | +---------------> |                      |     
     |   Azure Web App      |                   |   Project Oxford     |     
     |                      | <---------------+ |   LUIS               |     
     |                      |        5          |                      |     
     |                      |                   |                      |     
     +----------------------+                   +----------------------+     
                ^      |                                                     
                |      |                                                     
                |      |                                                     
                | 3    |6                                                    
                |      |                                                     
                |      |                                                     
                |      v                                                     
                |                                                            
     +----------------------+                   +------------------------+   
     |                      |     1             |                        |   
     |                      +-----------------> |                        |   
     |                      |                   |                        |   
     |   Browser            |                   |    Project Oxford      |   
     |                      | <-----------------+    Speech-to-text      |   
     |                      |       2           |                        |   
     |                      |                   |                        |   
     +----------------------+                   +------------------------+   
                                                                             
                                                                             
                         
                                                                                        
                                                                                        
                                                                                        

```
