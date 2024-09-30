using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

public class Login : MonoBehaviour
{
    private byte[] m_Ticket;
    private uint m_pcbTicket;
    private HAuthTicket m_HAuthTicket;
    private SteamNetworkingIdentity st;

    string sessionTicket = string.Empty;

    protected Callback<GetAuthSessionTicketResponse_t> m_GetAuthSessionTicketResponse;

    void OnGetAuthSessionTicketResponse(GetAuthSessionTicketResponse_t pCallback)
    {
        //Resize to buffer of 1024
        System.Array.Resize(ref m_Ticket, (int)m_pcbTicket);

        //format as Hex
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        foreach (byte b in m_Ticket) sb.AppendFormat("{0:x2}", b);

        sessionTicket = sb.ToString();
        Debug.Log("Hex encoded ticket: " + sb.ToString());
    }

    void Start()
    {
        //Login.Initialize(true);

        if (SteamManager.Initialized)
        {
            m_GetAuthSessionTicketResponse = Callback<GetAuthSessionTicketResponse_t>.Create(OnGetAuthSessionTicketResponse);
            st = new SteamNetworkingIdentity();
            m_Ticket = new byte[1024];
            m_HAuthTicket = SteamUser.GetAuthSessionTicket(m_Ticket, 1024, out m_pcbTicket, ref st);

        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
