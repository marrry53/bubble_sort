using System;

namespace bubble_sort
{
    class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            dll.list();
            //bubble.bubblesort();
            //qsd.quicksortdom();
            //qs.quicksortSmart();
            //xmpl.recursion();
            //exmpl.intermezzo();
            Console.ReadKey();
        }
        
        class dll
        {
            public static void list()
            {
                int[] x = { 5, 1, 9, 4 };
                //           1   4  5   9
                Console.WriteLine("array before moving stuff");
                qs.printarray(x);
                lijst L= new lijst();
                for (int i = 0; i < x.Length; i++)
                {
                    L.add2orderedlist(x[i]);
                }
                L.add2orderedlist(7);
                int[] y = L.read_from_head_to_tail();
                Console.WriteLine("array after moving stuff plus 7");
                qs.printarray(y);
                // 1 4 5 7 9
            }
        }

        class lijst
        {
            //object description
            //props
            node head, tail;
            //func
            /// <summary>
            /// add v to list in such a way that it stays ordered asc
            /// and return head
            /// </summary>
            /// <algo>
            ///  5 , 1 , 9 , 4
            ///  value <= head.value
            ///  value > tail.value
            ///  value somewhere in between: start at the head and 
            ///        find the node that has a higher value and
            ///        take care that all prev's and nxt's are valid.
            ///     1   5   9
            ///     4
            ///
            ///     1  3  5  7  9
            ///     7
            /// </algo>
            public node add2orderedlist(int v)
            {
                node n = new node(v);
                if (head == null)
                {
                    head = n; tail = n;
                }
                else
                {
                    //before head
                    if (n.v <= head.v)
                    {
                        head.prev = n;
                        n.nxt = head;
                        head = n;
                    }
                    else
                    {
                        //behind tail
                        if (n.v > tail.v)
                        {
                            tail.nxt = n;
                            n.prev = tail;
                            tail = n;
                        }
                        else
                        {
                            //look for the node that comes after me...
                            //somewhere in between
                            node after = head.nxt;
                            while (n.v > after.v)
                            {
                                after = after.nxt;
                            }
                            // Insert the new node before this one...
                            node before = after.prev;
                            before.nxt = n;
                            after.prev = n;
                            n.prev = before;
                            n.nxt = after;
                        }
                    }
                }
                return head;
            }
            /// <summary>
            /// put all elements of node in array
            /// </summary>
            /// <algo>
            /// declare int length and store length of array with get_count function
            /// declare array result with length from get_count
            /// start at the head
            /// when n isn't null
            /// put value of n.v in position of result[count]
            /// move to next node and increase count
            /// return result
            /// </algo>
            public int[] read_from_head_to_tail()
            {
                int length = get_count();
                int[] result = new int[length];
                node n = head;
                int count = 0;
                while (n != null)
                {
                    result[count++] = n.v;
                    n = n.nxt;
                }
                return result;
            }
            /// <summary>
            /// count nodes to define length
            /// </summary>
            /// <algo>
            /// start at the head
            /// declare count
            /// when n isn't null
            /// increase count and move to next node
            /// return count
            /// </algo>
            public int get_count()
            {
                node n = head;
                int count = 0;
                while (n != null)
                {
                    count++;
                    n = n.nxt;
                }
                return count;
            }
            //constructor
        }

        class node
        {
            //object description (cooking recipe, blueprint)
            //properties
            public int v;
            public node prev, nxt;
            //constructor
            public node(int v)
            {
                this.v=v;
            }
        }


        class qs
        {
            //hoare 1959
            // sort(x,start,end);
            public static void quicksortSmart()
            {
                int a = 5;
                int min = 0;
                int max = 10;

                int[] x = generate(a, min, max);

                //int[] x = new int[] { 2, 5 };
                //int[] x = new int[] { 5, 2, 7, 3, 9, 3, 0, 2, 8 };
                Console.WriteLine("Unsorted: ");
                printarray(x);
                quicksort(x, 0, x.Length - 1);
                Console.WriteLine("sorted: ");
                printarray(x);
            }
            //            H
            // y={5,2,7,5,1}
            //      L
            // 
            //            H
            // y={5,2,7,5,1}
            //        L
            //            H
            // y={5,2,1,5,7}
            //        L
            //          H
            // y={5,2,1,5,7}
            //            L

            //          H
            // y={5,2,1,5,7}   //we swapped the pivot with x[H]
            //            L
            //       H
            //  {5,2,1}
            //       L

            //       H
            //  {1,2,5}
            //       L


            // x = {5,2,5,1,7,3,8}
            // x=sort(x,2,4)  ==> x={5,2,1,5,7,3,8}
            //
            // x = sort(x, 0, x.Length-1);
            //
            // pivot first element 5
            // 
            //                  R   (search <= pivot)
            // {5,2,7,3,9,3,0,2,8}  (= x)
            //    L                 (search > pivot)
            // 
            //                R      (search <= pivot)
            // {5,2,7,3,9,3,0,2,8}   (= x)
            //      L                (search > pivot)
            //
            //                R      (search <= pivot)
            // {5,2,2,3,9,3,0,7,8}   (= x)
            //      L                (search > pivot)
            //
            //              R        (search <= pivot)
            // {5,2,2,3,9,3,0,7,8}   (= x)
            //          L            (search > pivot)
            //
            //              R        (search <= pivot)
            // {5,2,2,3,0,3,9,7,8}   (= x)
            //          L            (search > pivot)
            //
            //            R          (search <= pivot)
            // {5,2,2,3,0,3,9,7,8}   (= x)
            //              L        (search > pivot)
            //
            //            R          (search <= pivot)
            // {3,2,2,3,0,5,9,7,8}   (= x)
            //              L        (search > pivot)
            //
            // pivot is in the right location
            /// <summary>
            /// sort  x asc from beginning to end
            /// </summary>
            ///  <alcho>
            ///  while loop checking left <= right
            ///  x[left] ++ if condition of while is true, left smaller then pivot
            ///  or hasn't reached end
            ///  x[right]-- if condition is true
            ///  when right greater left, swap right and left
            ///  put pivot in right place
            /// divide pieces of array x before  and after right in recursion
            /// sort(x,beginning,right-1);
            /// sort(x,right+1,end);
            /// stopcondition = beginning greater or equal end then return x
            ///</alcho>
            public static int[] quicksort(int[] x, int beginning, int end)
            {
                if (beginning >= end) return x;
                int pivot = x[beginning];
                int left = beginning + 1;
                int right = end;
                while (left <= right)
                {
                    while (x[left] <= pivot && left < end)
                    {
                        left++;

                    }
                    while (x[right] > pivot)
                    {
                        right--;
                    }
                    if (right > left)
                    {
                        swap(x, right, left);
                    }
                }
                swap(x, beginning, right);
                quicksort(x, beginning, right - 1);
                quicksort(x, right + 1, end);
                return x;
            }
            public static void swap(int[] x, int right, int left)
            {
                int temp = x[right];
                x[right] = x[left];
                x[left] = temp;
            }
            public static int[] generate(int a, int min, int max)
            {
                int[] x = new int[a];
                for (int i = 0; i < a; i++)
                {
                    x[i] = rnd.Next(min, max);
                }
                return x;
            }

            public static int[] printarray(int[] x)
            {
                for (int i = 0; i < x.Length; i++)
                {
                    Console.WriteLine(x[i]);
                }
                return x;
            }
        }

        class exmpl
        {
            /// <summary>
            /// if with one statement
            /// </summary>
            public static void intermezzo()
            {
                if (false) Console.WriteLine("hoi ");
                else Console.WriteLine("curly bracket");
            }


            public static void recursion()
            {
                int r = Rmfac(5);
            }

            /// <summary>
            /// return n!
            /// </summary>
            /// <algo>
            /// recursive
            /// 
            /// 5! = 5*4*3*2 = 5*(4*3*2) =5 * 4!
            /// n!= n* (n-1)!
            /// </algo>
            static int Rmfac(int n)
            {
                if (n == 1) return 1;
                return n * Rmfac(n - 1);
            }

            /// <summary>
            /// return n!
            /// </summary>
            /// <algo>
            ///  5! = 5*4*3*2
            ///  3! = 3*2
            /// </algo>
            static int mfac(int n)
            {
                int p = 1;
                for (int i = 2; i <= n; i++)
                {
                    p *= i;
                }
                return p;
            }

        }

        class qsd
        {
            /// <summary>
            ///  use RQuicksort to sort x asc
            /// </summary>
            public static void quicksortdom()
            {
                int a = 5;
                int min = 0;
                int max = 10;

                int[] x = generate(a, min, max);

                //int[] x = new int[] {4, 4, 4, 1, 4, 4, 4, 7,4,6,3,2,7,12,3,4,5,6,98,2,3,4,5,6 };
                Console.WriteLine("Unsorted: ");
                printarray(x);
                RQuicksort(x);
                Console.WriteLine("Sorted");
                printarray(x);
            }

            /// <summary>
            /// return x asc sorted
            /// </summary>
            /// <algo>
            /// DIVIDE & CONQUER
            /// with respect to the average we divide 
            /// the array x in 2 arrays one with values <=average 'less'
            /// and one with values > average 'greater'
            /// 
            /// int[] x= {5,4,2,7,1,4}
            /// 
            /// 1. average =  3.83
            /// 2. count <= average and > average   ==>  small=2 big=4
            /// 
            /// 3. Create an array with lower or equal numbers  'less'
            /// 4. Create an array with higher numbers   'greater'
            /// 
            /// 5. fill both arrays with appropriate values
            ///     
            /// 6. recurse both arrays
            ///         less=Rquicksort(less);
            ///         greater=Rquicksort(greater);
            ///         
            /// 7. concat less and greater  back to x
            /// 8. return x;
            /// </algo>
            public static void RQuicksort(int[] x)
            {
                double average = CalculateAverage(x);
                int big = CountBiggerValues(x, average);
                if (big == 0) return;
                int small = x.Length - big;
                int[] less = new int[small];    // GetLowerValues(x, average);
                int less_index = 0;
                int[] greater = new int[big];  // GetHigherValues(x, average);
                int greater_index = 0;
                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] <= average) less[less_index++] = x[i];
                    else greater[greater_index++] = x[i];
                }
                RQuicksort(less);
                RQuicksort(greater);
                ConcatArrays(less, greater, x);

            }
            /// <summary>
            /// loop through array
            /// count bigger values
            /// </summary>
            public static int CountBiggerValues(int[] x, double average)
            {
                int result = 0;
                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] > average)
                    {
                        result++;
                    }
                }
                return result;
            }
            /// <summary>
            /// loop through array
            /// count smaller values
            /// </summary>
            public static int CountSmallerValues(int[] x, double average)
            {
                int result = 0;
                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] <= average)
                    {
                        result++;
                    }
                }
                return result;
            }
            /// <summary>
            /// return in result combined values of the arays less, greater in that order
            /// </summary> 
            /// <algo>
            /// loop through arrays less & greater
            /// concat arrays into result
            /// begin with adding less array to result
            /// count less.Length and result and add greater array
            /// </algo>
            public static void ConcatArrays(int[] less, int[] greater, int[] result)
            {
                for (int i = 0; i < less.Length; i++)
                {
                    result[i] = less[i];
                }
                for (int i = 0; i < greater.Length; i++)
                {
                    result[i + less.Length] = greater[i];
                }
            }
            /// <summary>
            /// Loop through all the numbers in the list
            /// and return the average.
            /// </summary>
            public static double CalculateAverage(int[] x)
            {
                double total = 0;
                for (int i = 0; i < x.Length; i++)
                {
                    total += x[i];
                }
                return total / x.Length;
                // a%b  =   a-b*(a/b)
                // 16%3 = 16 - 3* (16/3)
            }
            //print array
            public static int[] printarray(int[] x)
            {
                for (int i = 0; i < x.Length; i++)
                {
                    Console.WriteLine(x[i]);
                }
                return x;
            }
            public static int[] generate(int a, int min, int max)
            {
                int[] x = new int[a];
                for (int i = 0; i < a; i++)
                {
                    x[i] = rnd.Next(min, max);
                }
                return x;
            }

        }

        class bubble
        {
            static Random rnd = new Random();

            /// <summary>
            /// sort an array of integers asc
            /// </summary>
            public static void bubblesort()
            {
                int n = 5;
                int min = 0;
                int max = 10;

                int[] numbers = generate(n, min, max);
                //int[] numbers = new int[] { 1, 3, 7, 5, 9, 4, 8, 2, 3, 6};
                Console.WriteLine("Unsorted numbers:");
                printarray(numbers);
                numbers = bubble.bubbly(numbers);
                Console.WriteLine("Sorted numbers:");
                printarray(numbers);
            }

            /// <summary>
            /// return numbers asc sorted
            /// </summary>
            /// <algo>
            /// 2 9 1 5
            /// 2 1 5 9
            /// 
            /// for loop through array and put highest numbers at
            /// the end until all are asc ==> 
            /// 
            /// each time    the innerloop has been executed, you can execute
            /// the inner loop with one item less,  -k in the innerloop
            /// 
            /// -1 in the outer loop, because if 2 out of 3 are in the right 
            /// place, then the 3rd is alright too
            /// </algo>
            public static int[] bubbly(int[] numbers)
            {
                for (int k = 0; k < numbers.Length - 1; k++)
                {
                    for (int i = 0; i < numbers.Length - 1 - k; i++)
                    {
                        if (numbers[i] > numbers[i + 1])
                        {
                            bubble.swap(numbers, i, i + 1);
                        }
                    }
                }
                return numbers;
            }

            ///<summary>
            /// swap numbers[i] with numbers[j]
            /// </summary>
            /// <algo>
            /// put numbers[i] in temp, replace numbers[i] with [j] and replace numbers[j] with temp
            /// </algo>
            public static void swap(int[] numbers, int i, int j)
            {
                int temp = numbers[i];
                numbers[i] = numbers[j];
                numbers[j] = temp;
            }
            /// <summary>
            /// print numbers array
            /// </summary>
            ///<algo>
            /// for loop through numbers and print numbers[i]
            /// </algo>        
            public static int[] printarray(int[] numbers)
            {
                for (int i = 0; i < numbers.Length; i++)
                {
                    Console.WriteLine(numbers[i]);
                }
                return numbers;
            }

            /// <summary>
            /// return an int[] with n random values between min and max
            /// </summary>
            /// <algo>
            /// for loop
            /// </algo>
            public static int[] generate(int n, int min, int max)
            {
                int[] x = new int[n];
                for (int i = 0; i < n; i++)
                {
                    x[i] = rnd.Next(min, max);
                }
                return x;
            }
        }

    }
}
