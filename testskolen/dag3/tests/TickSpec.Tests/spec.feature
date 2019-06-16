Feature: Given apples should be added to the basket

Scenario: Given apples should be added to the basket
    Given I have 3 apples in the basket
    When Lars gives med 2 apples
    Then I should have 5 apples in the basket