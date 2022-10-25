using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp14
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create chatroom
            Chatroom chatroom = new Chatroom();
            // Create participants and register them
            Participant M1 = new Highschool("고등학생1");
            Participant M2 = new Highschool("고등학생2");
            Participant M3 = new Highschool("고등학생3");
            Participant M4 = new Highschool("고등학생4");
            Participant M5 = new NoN_Highschool("대학생1");
            chatroom.Register(M1);
            chatroom.Register(M2);
            chatroom.Register(M3);
            chatroom.Register(M4);
            chatroom.Register(M5);

            M5.Send("고등학생2", "고등학생 부럽다");
            M2.Send("고등학생3", "피시방갈래?");
            M3.Send("고등학생2", "오늘 급식 뭐냐?");
            M4.Send("고등학생1", "수학 숙제 했냐?");
            M4.Send("대학생1", "대학생 부럽다");

            Console.ReadKey();
        }
    }

    public abstract class AbstractChatroom
    {
        public abstract void Register(Participant participant);
        public abstract void Send(
            string from, string to, string message);
    }

    public class Chatroom : AbstractChatroom
    {
        private Dictionary<string, Participant> participants = new Dictionary<string, Participant>();
        public override void Register(Participant participant)
        {
            if (!participants.ContainsValue(participant))
            {
                participants[participant.Name] = participant;
            }
            participant.Chatroom = this;
        }
        public override void Send(string from, string to, string message)
        {
            Participant participant = participants[to];
            if (participant != null)
            {
                participant.Receive(from, message);
            }
        }
    }

    public class Participant
    {
        Chatroom chatroom;
        string name;

        public Participant(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
        }

        public Chatroom Chatroom
        {
            set { chatroom = value; }
            get { return chatroom; }
        }
  
        public void Send(string to, string message)
        {
            chatroom.Send(name, to, message);
        }

        public virtual void Receive(
            string from, string message)
        {
            Console.WriteLine("{0} to {1}: '{2}'",
                from, Name, message);
        }
    }

    public class Highschool : Participant
    {
        public Highschool(string name)
            : base(name)
        {
        }
        public override void Receive(string from, string message)
        {
            Console.Write("고등학생들에게 : ");
            base.Receive(from, message);
        }
    }

    public class NoN_Highschool : Participant
    {

        public NoN_Highschool(string name)
            : base(name)
        {
        }
        public override void Receive(string from, string message)
        {
            Console.Write("고등학생이 아닌 사람들에게 : ");
            base.Receive(from, message);
        }
    }
}
