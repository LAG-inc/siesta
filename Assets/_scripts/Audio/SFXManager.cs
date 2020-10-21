using UnityEngine;

public enum Sound
{
    deslizar,
    salto,
    choqueObjeto,
    ovni,
    checkpoint,
    meteorito
}

public class SFXManager : MonoBehaviour
{
    public static SFXManager SI;

    private void Awake()
    {
        SI = SI == null ? this : SI;
    }

    //Referencias a los audios source respectivos
    [SerializeField] private AudioSource deslizar, salto, choqueObjeto, ovni, checkpoint, meteorito;

    public void PlaySound(Sound soundToPlay)
    {
        switch (soundToPlay)
        {
            case Sound.deslizar:
                deslizar.PlayOneShot(deslizar.clip);
                break;
            case Sound.salto:
                salto.PlayOneShot(salto.clip);
                break;
            case Sound.choqueObjeto:
                choqueObjeto.PlayOneShot(choqueObjeto.clip);
                break;
            case Sound.ovni:
                ovni.PlayOneShot(ovni.clip);
                break;
            case Sound.checkpoint:
                checkpoint.PlayOneShot(checkpoint.clip);
                break;
            case Sound.meteorito:
                meteorito.PlayOneShot(meteorito.clip);
                break;
        }
    }
}