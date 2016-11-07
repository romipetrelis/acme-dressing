# Getting Started
* Clone/download the repo
* Open `src/Acme.sln`
* Build Solution
* `Run All` tests
* Run `Acme.Dressing.Web`

# User Story
In order to leave the house,   
As a Pajama Wearer in my house,    
I want to dress in temperature-appropriate clothes

# Scenario Outline: Dressing
Given a temperature type `<temperature type>` and a numeric command list `<commands>`  
When I process the commands `<commands>`  
Then I should get the expected result `<result>`  

## Examples:
| temperature type | commands | result |
| ---------------- | -------- | ------- |
| HOT | 8, 6, 4, 2, 1, 7 | Removing PJs, shorts, shirt, sunglasses, sandals, leaving house |
| COLD | 8, 6, 3, 4, 2, 5, 1, 7 | Removing PJs, pants, socks, shirt, hat, jacket, boots, leaving house |
| HOT | 8, 6, 6 | Removing PJs, shorts, fail |
| HOT | 8, 6, 3 | Removing PJs, shorts, fail |
| COLD | 8, 6, 3, 4, 2, 5, 7 | Removing PJs, pants, socks, shirt, hat, jacket, fail |
| COLD | 6 | fail |


# Commands
| Command | Description | Hot Response | Cold Response |
| ------- | ----------- | ------------ | ------------- |
| 1 | Put on footwear | sandals | boots |
| 2 | Put on headwear | sunglasses | hat |
| 3 | Put on socks | fail | socks |
| 4 | Put on shirt | shirt | shirt |
| 5 | Put on jacket | fail | jacket |
| 6 | Put on pants | shorts | pants |
| 7 | Leave house | leaving house | leaving house |
| 8 | Take off pajamas | Removing PJs | Removing PJs | 

# Rules
* You start in the house with your PJ's on
* Pajamas must be taken off before anything else can be put on
* Only 1 piece of each type of clothing may be put on
* You cannot put on socks when it is hot 
* You cannot put on a jacket when it is hot
* Socks must be put on before footwear
* Pants must be put on before footwear
* The shirt must be put on before the headwear or jacket
* You cannot leave the house until all items of clothing are on (except socks and a jacket when it's hot)
* If an invalid command is issued, respond with "fail" and stop processing commands

# Non-Functional Requirements
* Use C#
* Provide all source, test, and build support files
* Apply OO Design Principles
* Make sure code is legible
* Make code easy to maintain
* Use best practices and patterns
