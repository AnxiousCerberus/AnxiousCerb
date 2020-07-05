using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;

namespace TMPro.Examples
{

    public class RollingTextFade : MonoBehaviour
    {

        private TMP_Text m_TextComponent;

        //REMEMBER : It can't go faster than the current framerate
        //If need to go faster, increase Increment amount instead
        public float IncrementEverySeconds = 0.001f;

        public byte IncrementAmount = 10;
        public byte targetAlpha;
        public byte alphaTresholdBeforeNextChar = 125;
        public bool ready = false;

        void Awake()
        {
            m_TextComponent = this.gameObject.GetComponent<TMP_Text>();
            ready = true;
            DialogueManager.fadeController = this;
        }


        void Start()
        {
            //Use only for debug
            //StartCoroutine(AnimateVertexColors());
        }

        void FadeAlpha(int i, ref byte alpha, int vertexIndex)
        {
            Color32[] newVertexColors;
            TMP_TextInfo textInfo = m_TextComponent.textInfo;

            // Get the index of the material used by the current character.
            int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

            // Get the vertex colors of the mesh used by this text element (character or sprite).
            newVertexColors = textInfo.meshInfo[materialIndex].colors32;

            // Set new alpha values.
            alpha = (byte)Mathf.Clamp ((byte)alpha + IncrementAmount, 0, targetAlpha);

            newVertexColors[vertexIndex + 0].a = alpha;
            newVertexColors[vertexIndex + 1].a = alpha;
            newVertexColors[vertexIndex + 2].a = alpha;
            newVertexColors[vertexIndex + 3].a = alpha;

            // Upload the changed vertex colors to the Mesh.
            m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        }

        /// <summary>
        /// Method to animate vertex colors of a TMP Text object.
        /// </summary>
        /// <returns></returns>
        public IEnumerator AnimateVertexColors()
        {
            ready = false;
            bool updateThis = true;

            // Need to force the text object to be generated so we have valid data to work with right from the start.
            m_TextComponent.ForceMeshUpdate();

            TMP_TextInfo textInfo = m_TextComponent.textInfo;
            Color32[] newVertexColors;

            //Get Total Char count for this TMP
            int totalCharacterCount = textInfo.characterCount;
            int lastVisibleCharacter = 0;

            //TODO : REFACTO THIS
            for (int currentCharacter = 0; currentCharacter < totalCharacterCount; currentCharacter++)
            {
                //Set all text transparent before we begin fading in
                // Get the index of the first vertex used by this text element.
                // Get the index of the material used by the current character.
                int materialIndex = textInfo.characterInfo[currentCharacter].materialReferenceIndex;

                // Get the vertex colors of the mesh used by this text element (character or sprite).
                newVertexColors = textInfo.meshInfo[materialIndex].colors32;

                int vertexIndex = textInfo.characterInfo[currentCharacter].vertexIndex;

                newVertexColors[vertexIndex + 0].a = 0;
                newVertexColors[vertexIndex + 1].a = 0;
                newVertexColors[vertexIndex + 2].a = 0;
                newVertexColors[vertexIndex + 3].a = 0;

                //Get last visible character's number
                if (textInfo.characterInfo[currentCharacter].isVisible && lastVisibleCharacter < currentCharacter)
                    lastVisibleCharacter = currentCharacter;
            }

            UnityEngine.Debug.Log("Last visible character is = " + lastVisibleCharacter + "/" + totalCharacterCount);

            while (updateThis)
            {
                int currentCharCountToUpdate = 1;

                for (int currentCharacter = 0; currentCharacter < currentCharCountToUpdate; currentCharacter++)
                {
                    //Make sure our current count of chars that need an update does not go beyond the total character count of the currently displayed text
                    currentCharCountToUpdate = Mathf.Clamp(currentCharCountToUpdate, 0, totalCharacterCount);

                    // Skip characters that are not visible
                    if (!textInfo.characterInfo[currentCharacter].isVisible)
                    {
                        currentCharCountToUpdate++;
                        continue;
                    }

                    // Get the index of the material used by the current character.
                    int materialIndex = textInfo.characterInfo[currentCharacter].materialReferenceIndex;

                    // Get the vertex colors of the mesh used by this text element (character or sprite).
                    newVertexColors = textInfo.meshInfo[materialIndex].colors32;

                    // Get the index of the first vertex used by this text element.
                    int vertexIndex = textInfo.characterInfo[currentCharacter].vertexIndex;



                    // Get the current character's alpha value.
                    byte alpha = (byte)Mathf.Clamp(newVertexColors[vertexIndex + 0].a, 0, targetAlpha);

                    if (alpha < targetAlpha)
                    {
                        FadeAlpha(currentCharacter, ref alpha, vertexIndex);
                    }

                    if (alpha >= alphaTresholdBeforeNextChar)
                        currentCharCountToUpdate++;



                    // Get the index of the first vertex used by this text element.
                    int lastvisiblevertex = textInfo.characterInfo[lastVisibleCharacter].vertexIndex;

                    //Get it again after Coroutine updated it
                    byte lastVisibleAlpha = (byte)Mathf.Clamp(newVertexColors[lastvisiblevertex + 0].a, 0, targetAlpha);

                    if (currentCharacter >= lastVisibleCharacter)
                        UnityEngine.Debug.Log("Last Char is = " + textInfo.characterInfo[currentCharacter].character);

                    // If reached the last character of the total displayed rn, we can end our loop.
                    if (lastVisibleAlpha >= targetAlpha)
                    {
                        UnityEngine.Debug.Log("Alpha Fade ended with " + currentCharacter + "/" + lastVisibleCharacter + "/" + totalCharacterCount + " & Alpha = " + alpha);
                        UnityEngine.Debug.Log("FYI, LastVisibleCharacter " + lastVisibleCharacter + " => " + textInfo.characterInfo[lastVisibleCharacter].character + " Alpha was = " + lastVisibleAlpha);
                        updateThis = false;
                    }


                }
                yield return new WaitForSeconds(IncrementEverySeconds);
            }

            ready = true;
            yield break;
        }


    }
}
