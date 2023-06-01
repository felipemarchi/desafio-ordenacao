using System.Diagnostics;

var arquivo = new StreamReader("C:\\Users\\felipe.marchi\\Desktop\\desafio\\numeros-naturais.txt");
var lista = new List<int>();
string? row;

while ((row = arquivo.ReadLine()) != null)
    lista.Add(Convert.ToInt32(row));

var stopWatch = new Stopwatch();
stopWatch.Start();

#region ALGORITMO

int iComparacao = 0, iLimiteInicial, iLimiteFinal;
var listaOrdenada = new List<int>() { lista.First() };
lista.RemoveAt(0);

foreach (var item in lista)
{
    iLimiteInicial = -1;
    iLimiteFinal = -1;
    EncaixarItemNaNovaLista(item);
}

#endregion

stopWatch.Stop();
var timeSpan = stopWatch.Elapsed;

var tempo = string.Format("{0:00}:{1:00}.{2:00}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10);
Console.WriteLine($"FIM :. {tempo} / {listaOrdenada.First()} / {listaOrdenada.Last()}");

/// -------------------------------------------------

void EncaixarItemNaNovaLista(int item)
{
    while (true)
    {
        // 1. ITEM MAIOR QUE O INDEX COMPARADO - VAI PARA DIREITA
        if (item > listaOrdenada[iComparacao])
        {
            // A) Chegou ao fim da lista
            if (listaOrdenada.Count == iComparacao + 1)
            {
                listaOrdenada.Add(item);
                return;
            }

            // B) Deve ser inserido do lado direito do index comparado
            else if (item <= listaOrdenada[iComparacao + 1])
            {
                listaOrdenada.Insert(iComparacao + 1, item);
                return;
            }

            // C) Redefinir index de comparação para o meio da lista à direita
            else
            {
                iLimiteInicial = iComparacao;
                iLimiteFinal = iLimiteFinal < 0 ? listaOrdenada.Count : iLimiteFinal;

                var meioDireita = (iLimiteFinal - iComparacao) / 2;
                iComparacao += meioDireita;
            }
        }

        // 2. ITEM MENOR QUE O INDEX COMPARADO - VAI PARA ESQUERDA
        else
        {
            // A) Chegou ao início da lista
            if (iComparacao - 1 < 0)
            {
                listaOrdenada.Insert(iComparacao, item);
                return;
            }

            // B) Deve ser inserido do lado esquerdo do index comparado
            else if (item >= listaOrdenada[iComparacao - 1])
            {
                listaOrdenada.Insert(iComparacao, item);
                return;
            }

            // C) Redefinir index de comparação para o meio da lista à esquerda
            else
            {
                iLimiteFinal = iComparacao;
                iLimiteInicial = iLimiteInicial < 0 ? 0 : iLimiteInicial;

                var meioEsquerda = (iComparacao + 1 - iLimiteInicial) / 2;
                iComparacao -= meioEsquerda;
            }
        }
    }
}