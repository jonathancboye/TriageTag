from google.appengine.ext.webapp import template
import webapp2
import os

class MainHandler(webapp2.RequestHandler):
  def get(self):
    path = os.path.join('templates', 'index.html')
    self.response.out.write(template.render(path, {}))

app = webapp2.WSGIApplication([
    ('/', MainHandler),
  ], debug=True)
