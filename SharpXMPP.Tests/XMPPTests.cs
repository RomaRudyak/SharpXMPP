﻿using System.Diagnostics;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpXMPP.Client;
using SharpXMPP.Stream;

namespace SharpXMPP.Tests
{
    [TestClass]
    public class XMPPTests
    {
        static void TestJID(JID jid, string user, string domain, string resource)
        {
            Assert.AreEqual(user, jid.User);
            Assert.AreEqual(domain, jid.Domain);
            Assert.AreEqual(resource, jid.Resource);
        }
        [TestMethod]
        public void JIDTests()
        {
            TestJID(new JID("_vt@xmpp.ru/ololo"), "_vt", "xmpp.ru", "ololo");
            TestJID(new JID("icq.jabber.ru"), string.Empty, "icq.jabber.ru", null);
            TestJID(new JID("vasya@icq.org"), "vasya", "icq.org", null);
            Assert.AreEqual("vasya@icq.org", new JID("vasya@icq.org").ToString());
            Assert.AreEqual("icq.org", new JID("icq.org").ToString());
            Assert.AreEqual("icq.org/registered", new JID("icq.org/registered").ToString());
        }
        [TestMethod]
        public void DNSTests()
        {
            DnsResolver.ResolveXMPPClient("gmail.com").ForEach(r => Trace.WriteLine(r.Host + ":" + r.Port));
        }

        [TestMethod]
        public void Serialization()
        {
            var errorinput =
                XElement.Parse("<stream:error xmlns:stream=\"http://etherx.jabber.org/streams\"><not-well-formed xmlns=\"urn:ietf:params:xml:ns:xmpp-streams\" /></stream:error>");
            var error = (Error)new XmlSerializer(typeof(Error)).Deserialize(errorinput.CreateReader());
            Assert.AreEqual(Stream.StreamError.NotWellFormed, error.ErrorType);
        }

    }
}