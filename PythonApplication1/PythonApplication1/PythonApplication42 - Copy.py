import urllib
import urllib.request

#from urllib.request import urlopen
#from urllib.parse import urlencode

response = urllib.request.urlopen('https://www.atmarkit.co.jp/ait/subtop/di')
print('url:', response.geturl())
print('code:', response.getcode())
print('Content-Type:', response.info()['Content-Type'])

print(response.info().get_content_charset())

content = response.read()
#print(content)
response.close()

#html = content.decode('shift_jis')
#print(html)
