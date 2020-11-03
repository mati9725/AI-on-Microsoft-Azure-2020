# Azure Cognitive Language Services

## Content Moderator

### 1. Wstęp
Contetnt Moderator to narzędzie do automatycznego wspomagania moderacji treści tekstowych i wizualnch. 
W zwracanej odpowiedzi może znajdować się rekomendacja sprawdzenia treści przez człowieka.
API usługi przyjmuje fragmenty tekstu i analizuje je wykrywając  wulgaryzmy.
Ponad to wykrywane mogą być również dane osobowe (np. numery telefonów i adresy email) lub słowa ze zdefiniowanej listy stworzonej przez użytkownika.
Usługa obsługuje różne języki i mogą one być rozpoznawane automatycznie.
Do Content Moderatora można również przesłyć obrazy w celu wydobycia z nich tekstu (OCR), wykrycia twarzy lub treści rasistowskich i erotycznych.

### 2. Przypadki użycia

* Moderacja treści na forach dyskusyjnych- blokowanie postów z niedozwolonymi treściami lub kierowanie ich do zatwierdzenia przez człowieka (moderatora) przed publikacją
* Filtrowanie w sklepach internetowych niecenzuralnych komentarzy lub negatywnych opinii o produkcie (z wykorzystaniem niestandardowych list wyszukiwanych słów)
* Wykrywanie w wiadomościach przekazywania danych kontaktowych gdy jest to niezgodne z regulaminem serwisu (np. BlaBlaCar)

### 3. Użytkowanie

**API**

Usługę Content Moderator wywołuje się przez api HTTP. Przesyłane teksty mogą mieć długość maksymalnie 1024 znaków, a obrazy rozmiar od 128 pikseli do 4MB. Przekazywanie obrazów odbywa się przez przesłanie linka, z którego obraz może zostać pobrany.

**Cennik**

* Plan darmowy umożliwia wysłanie 5000 zapytań miesięcznie z częstotliwością ograniczoną do 1 zapytania na sekundę. Możliwe jest posiadanie tylko jednej instancji w planie darmowym.
* W planie standardowym cena jest zależna od liczby wykonywanych zapytań. Za pierwszy milion zapytań cena wynosi 1$ za 1000 zapytań i spada progowo aż do 0,40$ za 1000 wywołań API po przekroczeniu 10 milionów zapytań.

## Language Understanding Intelligent Service (LUIS)

### 1. Wstęp

LUIS to usługa pozwalająca na rozpoznawanie znaczenia wypowiedzi sformułowanych w języku naturalnym. 
Konfiguracja usługi polega na stworzeniu listy intencji i przypisanych do nich wyrażeń oraz encji, które mają być wykrywane.
Po przesłaniu do usługi fragmentu tekstu zwracana jest lista skonfigurowanych intencji wraz z oceną (od 0 do 1) dopasowania każdego z nich. 

### 2. Przypadki użycia

* Rozpoznawanie intencji poleceń wyydawanych systemowi inteligentnego domu
* Przydzielanie klienta do odpowiedniego konsultatna po opisaniu swojej potrzeby mową ludzką
* Proponowanie klientowi produktu na podstawie słownego opisu jego potrzeb

### 3. Użytkowanie

**API**

LUIS odpowiada na zapytania HTTP. Odpowiedź zawiera listę skonfigurowanych intencji wraz z oceną ich jakości dopasowania wyrażenia do nich.
Tworzenie bazy intencji i wyrażeń odbywa się przez stronę internetową.
Model może być dotrenowywany wielokrotnie

**Cennik**

* Plan darmowy umożliwia wysłanie 10000 zapytań tekstowych miesięcznie z częstotliwością nie większą niż 5 transakcji  na sekundę i możliwe są jedynie zapytania tekstowe.
* W planie standardowym dodatkowo możliwe są zapytania głosowe. Kosztuja one 5,50$, a zapytania tekstowe 1,5$ za 1000 zapytań

## Text Analytics

### 1. Wstęp

Usługa Text Analytics umożliwia ocenę nastroju piszącego na podstawie wypowiedzi. 
Jednorazowo możemy pezesłać wiele wypowiedzi w różnych jezykach.
Innymi funkcjami tej usługi są: rozpoznawanie języka w jakim został napisany tekst, wybieranie z tekstu słów/wyrażeń kluczowych oraz encji.
Ostatnia z wmienionych funkcji zwraca linki do artykułów na wikipedii opisujących wykryte encje.

### 2. Przypadki użycia

* Klasyfikacja opinii na temat produktu w sklepie internetowym i ukrywanie negatywnych, a eksponowanie pozytywnych.
* Podział pilności obsługi zgłoszeń tak, żeby najpierw obsłużyć najbardziej niezadowolonych klientów
* Dobór proponowanych treści (np. muzyki) na podstawie nastroju

### 3. Użytkowanie

**API**

API usługi wywołujemy przez HTTP. Do funkcji rozpoznawania nastroju przekazuje się listę tekstów  podając do każdego język i unikatowy (w skali listy) identyfikator. W odpowiedzi otrzymójemy te same identyfikatory wraz z oceną z zakresu od 0 do 1. Ocena zbliżona do 0 oznacza nastrój negatywny, do 1 pozytywny, a około 0,5 neutralny.

**Cennik**

* Plan darmowy umożliwia wykonanie 5000 transakcji miesięcznie za darmo
* W planie standard cena jednostkowa jest tym mniejsza im bardziej wykorzystujemy usługę. Np. przetworzenie pierwszych 500000 rekordów kosztuje 2$ za 1000 rekordów, a powyżej 10 mln 0,25$ za 1000 rekordów.
* Dostępne są również plany od S0 do S4 z ustalonym abonamentem miesięcznym w ramach którego użytkownik ma do wykonania określoną ilość transakcji. Plany te (w przeciwieństwie do wymienionych wcześniej) umożliwiają korzystanie z funkcji Named Entity Recognition w kontenerze.
