Feature: BerlinClockInputValidation
	Validation of input data

Scenario: Random string
When the time is "adasdasd"
Then the clock should look like 
"""
Y
OOOO
OOOO
OOOOOOOOOOO
OOOO
"""

Scenario: Random language type
When the time is "null"
Then the clock should look like 
"""
Y
OOOO
OOOO
OOOOOOOOOOO
OOOO
"""

Scenario: Special chars
When the time is "!@#$!@#!@#"
Then the clock should look like 
"""
Y
OOOO
OOOO
OOOOOOOOOOO
OOOO
"""

Scenario: Empty input
When the time is ""
Then the clock should look like 
"""
Y
OOOO
OOOO
OOOOOOOOOOO
OOOO
"""