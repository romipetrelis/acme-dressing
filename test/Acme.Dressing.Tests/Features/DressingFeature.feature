Feature: Getting Dressed
	In order to leave the house  
	As a Pajama Wearer in my house   
	I want to dress in temperature-appropriate clothes

@integration
Scenario Outline: Process dressing commands
Given a temperature type <temperaturetype> 
And a CSV command list <commands>
When I process the commands 
Then I should get the expected result <result>

Examples:
| description                                        | temperaturetype | commands               | result                                                               |
| good HOT                                           | HOT             | 8, 6, 4, 2, 1, 7       | Removing PJs, shorts, shirt, sunglasses, sandals, leaving house      |
| good COLD                                          | COLD            | 8, 6, 3, 4, 2, 5, 1, 7 | Removing PJs, pants, socks, shirt, hat, jacket, boots, leaving house |
| Only1PieceOfEachTypeOfClothingMayBePutOn           | HOT             | 8, 6, 6                | Removing PJs, shorts, fail                                           |
| YouCannotPutOnSocksWhenItIsHot                     | HOT             | 8, 6, 3                | Removing PJs, shorts, fail                                           |
| YouCannotPutOnJacketWhenItIsHot                    | HOT             | 8, 5                   | Removing PJs, fail                                                   |
| YouCannotLeaveTheHouseUntilAllItemsOfClothingAreOn | COLD            | 8, 6, 3, 4, 2, 5, 7    | Removing PJs, pants, socks, shirt, hat, jacket, fail                 |
| PajamasMustBeTakenOffBeforeAnythingElseCanBePutOn  | COLD            | 6                      | fail                                                                 |
| SocksMustBePutOnBeforeFootwear                     | COLD            | 8, 1, 3                | Removing PJs, boots, fail                                            |
| PantsMustBePutOnBeforeFootwear                     | HOT             | 8, 1, 6                | Removing PJs, sandals, fail                                          |
| ShirtMustBePutOnBeforeHeadwear                     | HOT             | 8, 2, 4                | Removing PJs, sunglasses, fail                                       |
| ShirtMustBePutOnBeforeJacket                       | COLD            | 8, 5, 4                | Removing PJs, jacket, fail                                           |
| bad temp type                                      | MILD            | 8                      | fail                                                                 |
| bad command                                        | COLD            | 9                      | fail                                                                 |
