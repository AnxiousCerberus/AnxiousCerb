INCLUDE Test2.ink
INCLUDE Connie_Test.ink
// Max 95 char per line, plz 

VAR talkedOnce = false

=== TEST_SUBSCENE ===
{- talkedOnce == false:
     -> Talk_first
    - else:
     -> Talk_second
}

= Talk_first
~ talkedOnce = true
# Speaker: Sandy
"Heya!"
# Speaker: Ananda
"Heya Yourself!"
-> DONE

= Talk_second
# Speaker: Sandy
"Oh, it's you again! How's life?"
"Here's some more text to make sure that, you know..."
"The hashtag is taken into account properly."
"That would be great don't you think ?"
# Speaker: Ananda
"Yeah, most definitely."
"Well, let's hope the parsing did its job properly!"
"Anyway, got... Stuff to do, I guess. Soooo, see ya!"
-> DONE