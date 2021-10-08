Feature: Search
	Simple calculator for adding two numbers

@web
Scenario: Search for a keyword
	Given that I am on the Google page
	Then I click on the search input field
	Then I type in keyword
	Then I press Enter
	Then I verify result statistics are displayed
	Then I close the browser