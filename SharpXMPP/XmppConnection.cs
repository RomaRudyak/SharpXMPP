﻿using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using SharpXMPP.Client;

namespace SharpXMPP
{
    public class ConnFailedArgs
    {
        public string Message { get; set; }
    }

    public class ElementArgs
    {
        public bool IsInput { get; set; }
        public XElement Stanza { get; set; }
    }

    public class SignedInArgs
    {
        public JID ConnectionJID { get; set; }
    }

    public abstract class XmppConnection
    {
        public JID ConnectionJID { get; set; }

        public delegate void ConnectionFailedHandler(object sender, ConnFailedArgs e);

        public event ConnectionFailedHandler ConnectionFailed = delegate {};

        protected void OnConnectionFailed(ConnFailedArgs e)
        {
            ConnectionFailed(this, e);
        }

        public delegate void SignedInHandler(object sender, SignedInArgs e);

        public event SignedInHandler SignedIn = delegate {};

        protected void OnSignedIn(SignedInArgs e)
        {
            SignedIn(this, e);
        }

        public delegate void ElementHandler(object sender, ElementArgs e);

        public event ElementHandler Element = delegate {};

        protected void OnElement(ElementArgs e)
        {
            Element(this, e);
        }

        public delegate void IqHandler(object sender, Iq e);

        public event IqHandler Iq = delegate {};

        protected void OnIq(Iq e)
        {
            Iq(this, e);
        }

        public T Deserealize<T>(XElement input)
        {
            return (T)new XmlSerializer(typeof(T)).Deserialize(input.CreateReader());
        }

        
        public abstract XElement NextElement();

        public abstract void Send(XElement data);

        public abstract void MainLoop();
    }
}