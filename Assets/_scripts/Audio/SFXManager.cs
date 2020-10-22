using UnityEngine;

public enum Sound
{
    deslizar,
    salto,
    choqueObjeto,
    checkpoint,
    meteorito,
    caer,
    ovniLlegada,
    ovniSalida,
    ovniDetenido
}

public class SFXManager : MonoBehaviour
{
    public static SFXManager SI;

    private void Awake()
    {
        SI = SI == null ? this : SI;
    }

    //Referencias a los audios source respectivos
    [SerializeField] private AudioSource deslizar, salto, choqueObjeto, checkpoint, meteorito, caer, ovniLlegada, ovniSalida, ovniDetenido;

    public void PlaySound(Sound soundToPlay)
    {
        switch (soundToPlay)
        {
            case Sound.deslizar:
                if(!deslizar.isPlaying) deslizar.PlayOneShot(deslizar.clip);
                break;
            case Sound.salto:
                salto.PlayOneShot(salto.clip);
                break;
            case Sound.choqueObjeto:
                choqueObjeto.PlayOneShot(choqueObjeto.clip);
                break;
            case Sound.ovniLlegada:
                ovniLlegada.PlayOneShot(ovniLlegada.clip);
                break;
            case Sound.checkpoint:
                checkpoint.PlayOneShot(checkpoint.clip);
                break;
            case Sound.meteorito:
                if (!meteorito.isPlaying) meteorito.PlayOneShot(meteorito.clip);
                break;
            case Sound.caer:
                caer.PlayOneShot(caer.clip);
                break;
            case Sound.ovniSalida:
                ovniSalida.PlayOneShot(ovniSalida.clip);
                break;
            case Sound.ovniDetenido:
                ovniDetenido.PlayOneShot(ovniDetenido.clip);
                break;
        }
    }

    private void Update()
    {
        if (GameManager.SI.currentGameState != GameState.InGame) return;

        if (!PlayerInput.SI.IsJumping)
        {
            PlaySound(Sound.deslizar);
        }
        else{
            deslizar.Stop();
        }
    }
}