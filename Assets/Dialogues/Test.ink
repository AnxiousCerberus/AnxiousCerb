VAR talkedOnce = false

=== TEST_SUBSCENE ===
{- talkedOnce == false:
     -> Talk_first
    - else:
     -> Talk_second
}

= Talk_first
~ talkedOnce = true
# Sandy
"Heya!"
# Ananda
"Heya Yourself!"
-> DONE

= Talk_second
# Sandy
"Oh, it's you again! How's life?"
-> DONE