#region

using Unity.Netcode.Components;

#endregion

public class ClientNetworkAnimator : NetworkAnimator
{
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}