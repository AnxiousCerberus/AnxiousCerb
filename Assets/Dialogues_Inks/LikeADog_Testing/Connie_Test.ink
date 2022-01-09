=== Connie_Test ===

//#MOVE: Ananda > POSARRIVEE
{Connie_Test_Sub1: -> Connie_Test_Sub1} 
{Connie_Test_Sub2: -> Connie_Test_Sub2}

# Speaker: Connie
Ani !
# Speaker: Ananda
J’ai hésité à faire semblant de descendre les poubelles et m’étonner de te trouver là, mais…
# Speaker: Connie
J’ai cru que tu viendrais pas !

+{not SUPPRESS_CHOICES} Je t'emmerde d'façon -> Connie_Test_Sub1
+{not SUPPRESS_CHOICES} [Bah faut bien sortir les poubelles hé] -> Connie_Test_Sub2
-> DONE


=== Connie_Test_Sub1 ===
# Speaker: Ananda
T'es qu'une grosse conne
-> SUPPRESS_CHOICES

=== Connie_Test_Sub2 ===
# Speaker: Ananda
Sinon ça pue
-> SUPPRESS_CHOICES

=== SUPPRESS_CHOICES ===
The END
-> END

=== Salut_David ===
# Speaker: David
COUCOU LOL
-> DONE

=== JteParlePlus ===
J'te parle plus, connasse.
c toi ki pu lolilol
-> END