[System.Serializable]
public class LeverEffect
{
    public int leverIndex; // Índice da alavanca que afeta
    public Lever[] linkedLevers; // Alavancas que serão afetadas por esta alavanca
}
