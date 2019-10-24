from urllib import request  # urllib.requestモジュールをインポート
from bs4 import BeautifulSoup  # BeautifulSoupクラスをインポート

url = 'https://www.atmarkit.co.jp/ait/subtop/di/'
response = request.urlopen(url)
soup = BeautifulSoup(response)
response.close()

# 得られたsoupオブジェクトを操作していく

# print(soup)

# print(soup.title)
# print(soup.head.title)

# print(soup.title.text)

topstories = soup.find('div', class_='colBoxTopstories')
# print(topstories)

colboxindexes = topstories.find_all('div', class_='colBoxIndex')
# print(colboxindexes[0])

# title = colboxindexes[0].select('div.colBoxTitle')[0].text
# description = colboxindexes[0].select('div.colBoxDescription')[0].text
# print(title, ':', description)

top_articles = []
for item in colboxindexes:
    title = item.select('div.colBoxTitle')[0].text
    description = item.select('div.colBoxDescription')[0].text
    top_articles.append(f'{title}: {description}')

for articles in top_articles:
    print(articles)