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
"Here's some more text to make sure that, you know..."
"The hashtag is taken into account properly."
"That would be great don't you think ?"
# Ananda
"Yeah, most definitely."
"Well, let's hope the parsing did its job properly!"
"Anyway, got... Stuff to do, I guess. Soooo, see ya!"
-> DONE