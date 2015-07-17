using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PanelClickGame
{
    class Model
    {
        int MaxNumber;
        int CurrentNumber;
        long GameStartTick,GameEndTick;

        public Model(int max)
        {
            this.MaxNumber = max;
            this.Reset();
        }

        public long ElapsedTick
        {
            get
            {
                if (this.CurrentNumber == 0)
                    return 0;
                else if (this.CurrentNumber < this.MaxNumber)
                    return DateTime.Now.Ticks - this.GameStartTick;
                else
                    return this.GameEndTick - this.GameStartTick;
            }
        }

        public IEnumerable<int> NumberList()
        {
            List<int> numbers = Enumerable.Range(1, this.MaxNumber).ToList();
            Random rnd = new Random();
            while (numbers.Count > 0)
            {
                int index = rnd.Next(numbers.Count);
                yield return numbers[index];
                numbers.RemoveAt(index);
            }
        }

        public bool Click(int number)
        {
            if (number ==  this.CurrentNumber + 1)
            {
                this.CurrentNumber = number;
                if(this.CurrentNumber == this.MaxNumber)
                    this.GameEndTick = DateTime.Now.Ticks;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            this.CurrentNumber = 0;
            this.GameEndTick = DateTime.Now.Ticks;
            this.GameStartTick = DateTime.Now.Ticks;
        }
    }
}
