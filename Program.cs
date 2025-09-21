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
class StackDemo
{

	static void Main(string[] args)
	{
		MyStack st;

    Main:
		//MyStack st = new MyStack(100); Размер стека. При необходимости:
		try
		{
			Console.WriteLine("Введите размер стека:");
			int n = Convert.ToInt32(Console.ReadLine());
			st = new MyStack(n);
		}
		catch (Exception) { Error("Введено неверное значение!"); goto Main; }
		Console.Clear();
	Start:
		if (!st.Empty())
		{Console.WriteLine("Актуальный стек: "); ShowStack(st); Console.WriteLine(); }
		else Console.WriteLine("Лабораторная работа 2. Стеки");
		string str = "";
		Console.WriteLine("Введите, что вы хотите сделать?\n" +
			"1. Ввод значений в стек\n" +
			"2. Выполнить задание программы\n" +
			"3. Завершить программу\n" +
			"4. Изменить размер стека (актуальный стек удалится!)");
		int navigation;
		if (Int32.TryParse(Console.ReadLine(), out navigation))
			switch (navigation)
			{
				case 1:
					{
						goto FormattingStack;
					}
				case 2:
					{
						goto LabsMission;
					}
				case 3:
					{
						goto End;
					}
				case 4:
					{
						goto Main;
					}
				default:
					{
						Error("Неверно введён вариант ответа");
						goto Start;
					}
			}
		else { Error("Неверно введён вариант ответа"); goto Start; }
	FormattingStack:
        /*Console.Clear();
		st = new MyStack(100);
		Console.WriteLine("Ввод данных о стеке  (Нажмите Enter с пустым значением, чтобы завершить работу. Ввод стека до 100 значений)");
		do
		{
			try
			{
				Console.Write("Введите значение: ");
				str = Console.ReadLine();
				if (str != "")
				{
					st.Push(Convert.ToInt32(str));
				}
			}
			catch (OverflowException) { Console.WriteLine("Введено слишком большое значение значение! (максимум 2 147 483 647)"); continue; }
			catch (FormatException) { Console.WriteLine("Введено неверное значение!"); continue; }
			catch (IndexOutOfRangeException) { Console.WriteLine("Превышен лимит массива!"); goto Start; }
		}
		while (str != "");
		Console.Clear();*/
        Console.Clear();
        StackRedactor(st);
		goto Start;
	LabsMission:
		Console.Clear();
		if (st.Count() > 0)
		{
			Console.WriteLine("Исходный стек: "); ShowStack(st); Console.WriteLine();
			if (st.Count() != 1)
			{
				MyStack tempStack = new MyStack(st.Count());
				int max = int.MinValue;
				while (!st.Empty())
				{
					int elem = st.Pop();
					if (elem > max) max = elem;
					tempStack.Push(elem);
				}
				bool found = false;
				while (!tempStack.Empty())
				{
					int elem = tempStack.Pop();
					if (elem == max && !found) found = true;
					else
						st.Push(elem);
				}
				int cnt = (st.Count()) / 2;
				PushPop(cnt, tempStack, st);
				tempStack.Push(max);
				PushPop(cnt + 1, st, tempStack);
			}
			Console.WriteLine("Новый стек: "); ShowStack(st);
			Console.WriteLine();
		}
		else Console.WriteLine("Стек пустой, заполните его элементами!\n");
		goto Start;
	End:
		Console.Write("Программа завершила свою работу\nНажмите любую клавишу, чтобы завершить программу..."); Console.ReadLine();
		return;
	}
	static void PushPop(int k, MyStack stack1, MyStack stack2)
	{
		for (int i = 0; i < k; i++)
			stack1.Push(stack2.Pop());
	}
	static void ShowStack(MyStack stack)
	{
		int elem, k = stack.Count();
		MyStack temp = new MyStack(k);
		for (int i = 0; i < k; i++)
		{
			elem = stack.Pop();
			Console.Write(elem + " ");
			temp.Push(elem);
		}
		for (int i = 0; i < k; i++)
		{
			stack.Push(temp.Pop());
		}
	}
	

	static void StackRedactor(MyStack stack)
	{
		MyStack temp = stack;
		bool check;
    Start:
		if (!temp.Empty()){
		Console.WriteLine("Текущий стек: "); ShowStack(temp);
		}
		else Console.WriteLine("На данный момент стек пустой");
		Console.WriteLine("\nЧто вы хотите сделать?\n" +
			"1. Добавить элемент\n" +
			"2. Удалить элемент\n" +
			"3. Изменить элемент\n" +
			"4. Закончить изменения");
		int navigation;
		if (Int32.TryParse(Console.ReadLine(), out navigation))
			switch (navigation)
			{
				case 1:
					{
						goto AddToStack;
					}
				case 2:
					{
						goto RemoveFromStack;
					}
				case 3:
					{
						goto ReplaceElement;
					}
				case 4:
					{
						goto End;
					}
				default:
					{
						Error("Неверно введён вариант ответа");
						goto Start;
					}
			}
		else { Error("Неверно введён вариант ответа"); goto Start; }
	AddToStack:
        Console.WriteLine("Введите элемент, который хотите добавить в стек (пустая строка возвращает обратно в меню)");
        try
        {
            string str = Console.ReadLine();
            if (str != "")
            {
                temp.Push(Convert.ToInt32(str));
            }
            Console.Clear();
        }
        catch (OverflowException) { Error("Введено слишком большое значение значение! (максимум 2 147 483 647)"); }
        catch (FormatException) { Error("Введено неверное значение!"); }
        catch (IndexOutOfRangeException) { Error("Превышен лимит стека!");}
        goto Start;
	RemoveFromStack:
		try
		{
			Console.WriteLine("Будет удалён элемент " + temp.Peek() + " из стека. Вы уверены? (y/n)");
			string buf = Console.ReadLine();
            check = buf == "y" || buf == "yes";
			Console.Clear();
			if (check)
			{
				Console.WriteLine("Был удалён элемент " + temp.Pop() + "\n");
			}
			else Console.WriteLine("Действие отклонено\n");
		}
		catch (IndexOutOfRangeException)
		{
            Error("Список пуст, удалять нечего!");
		}
		goto Start;
	ReplaceElement:
		try
		{
            Console.WriteLine("Изменяется элемент " + temp.Peek() + ". Введите значение, на которое хотите поменять данный элемент (Пустая строка вернёт обратно в меню)");
            string str = Console.ReadLine(); Convert.ToInt32(str);
			if (str != "")
			{
				Console.WriteLine("Будет удалён элемент " + temp.Peek() + " из стека. Вы уверены? (y/n)");
				check = Console.ReadLine() == "y" || Console.ReadLine() == "yes";
				Console.Clear();
                if (check)
				{
					temp.Pop(); temp.Push(Convert.ToInt32(str));
					Console.WriteLine("Элемент изменён\n");
				}
				else Console.WriteLine("Действие отклонено\n");
			}
		}
		catch (OverflowException) { Error("Введено слишком большое значение значение! (максимум 2 147 483 647)"); }
		catch (FormatException) { Error("Введено неверное значение!"); }
		catch (IndexOutOfRangeException) { Error("Стек пустой, изменять нечего!"); }
		goto Start;
	End:
		Console.Clear();
		stack = temp;
		return;
	}
	static void Error(string Error) 
	{
		Console.Clear();
        Console.WriteLine(Error + "\n");
	} 
}
