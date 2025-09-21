using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Lab2
{
	
	public partial class Form1 : Form
	{
        MyStack stack = new MyStack(0);
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
            try
            {
                int value;
                if (Int32.TryParse(Interaction.InputBox("Введите значение в стек (целое чилсло до 2 147 483 647)", "Ввод значения", "0"), out value))
                {
                    stack.Push(value);
                    label2.Text = "Текущий стек: " + ShowStack(stack);
                    //Дальше балуемся с интерфейсом
                }
                else MessageBox.Show("Действие отклонено или введено некорректное значение!");
            }
            catch (IndexOutOfRangeException){ MessageBox.Show("Действие отклонено. Лимит стека превышен!"); }
        }

        private void button5_Click(object sender, EventArgs e)
		{
            int n;
            if (Int32.TryParse(textBox1.Text, out n)) {
                stack = new MyStack(n);
                label2.Text = "Здесь будет выведен стек (на данный момент стек пустой)";
                label1.Text = "Размер стека равен " + n.ToString();
            }
            else MessageBox.Show("Действие отклонено. Введено некорректное значение!");
            //Меняет размер стека до нужного нам, обнуляя имеющийся
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show($"Вы действительно хотите удалить элемент {stack.Peek()} из стека?", "Удаление элемента", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString() == "Yes")
                {
                    stack.Pop();
                    string st = ShowStack(stack);
                    label2.Text = (st == "") ? "Здесь будет выведен стек (на данный момент стек пустой)" : "Текущий стек: " + st;
                }
            }
            catch (IndexOutOfRangeException) { MessageBox.Show("Стек пустой. Введите значения в стек!"); }
        }

        private void button6_Click(object sender, EventArgs e)
		{
            try
            {
                int arg;
                if (MessageBox.Show($"Вы действительно хотите изменить элемент {stack.Peek()} из стека?", "Удаление элемента", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString() == "Yes")
                    if (Int32.TryParse(Interaction.InputBox("Введите значение, на которое хотите поменять (целое чилсло до 2 147 483 647)", "Ввод значения", "0"), out arg))
                    {
                        stack.Pop(); stack.Push(arg);
                        label2.Text = "Текущий стек: " + ShowStack(stack);

                        //Дальше балуемся с интерфейсом
                    }
                    else
                        MessageBox.Show("Действие отклонено. Введено некорректное значение");
            }
            catch (IndexOutOfRangeException) { MessageBox.Show("Стек пустой. Введите значения в стек!"); }
        }

		private void button2_Click(object sender, EventArgs e)
		{
            if (stack.Count() > 0)
            {
                label2.Text = "Исходный стек: " + ShowStack(stack);
                if (stack.Count() != 1)
                {
                    MyStack tempStack = new MyStack(stack.Count());
                    int max = int.MinValue;
                    while (!stack.Empty())
                    {
                        int elem = stack.Pop();
                        if (elem > max) max = elem;
                        tempStack.Push(elem);
                    }
                    bool found = false;
                    while (!tempStack.Empty())
                    {
                        int elem = tempStack.Pop();
                        if (elem == max && !found) found = true;
                        else
                            stack.Push(elem);
                    }
                    int cnt = (stack.Count()) / 2;
                    PushPop(cnt, tempStack, stack);
                    tempStack.Push(max);
                    PushPop(cnt + 1, stack, tempStack);
                }
                label2.Text += "\nНовый стек: " + ShowStack(stack);
            }
            else MessageBox.Show("Стек пустой. Введите значение в стек!");
            //выполнение задания
        }

		private void button3_Click(object sender, EventArgs e)
		{
			System.Windows.Forms.Application.Exit(); //сделано
		}
        static string ShowStack(MyStack stack)
        {
            int elem, k = stack.Count();
            string res = "";
            MyStack temp = new MyStack(k);
            for (int i = 0; i < k; i++)
            {
                elem = stack.Pop();
                res += elem.ToString() + " ";
                temp.Push(elem);
            }
            for (int i = 0; i < k; i++)
            {
                stack.Push(temp.Pop());
            }
            return res;
        }
        static void PushPop(int k, MyStack stack1, MyStack stack2)
        {
            for (int i = 0; i < k; i++)
                stack1.Push(stack2.Pop());
        }
    }
    class MyStack
    {
        int maxSize;
        int[] s;
        int stackCount = -1;
        public MyStack(int max_Size)
        {
            maxSize = max_Size;
            s = new int[maxSize];
        }
        public void Push(int x)
        {
            if (stackCount == maxSize - 1) throw new IndexOutOfRangeException();
            s[++stackCount] = x;
        }
        public int Pop()
        {
            if (!Empty())
                return s[stackCount--];
            throw new IndexOutOfRangeException();
        }
        public bool Empty()
        {
            return (stackCount == -1);
        }
        public int Count()
        {
            return stackCount + 1;
        }
        public int Peek()
        {
            if (Empty()) throw new IndexOutOfRangeException();
            return s[stackCount];
        }
    }
}
