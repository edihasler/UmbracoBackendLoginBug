# UmbracoBackendLoginBug
Steps to reproduce:
- cd nuxt
- pnpm i
- pnpm run build
- pnpm run preview - runs node server on localhost:3000
- run the backend aspnet application, where umbraco is running - runs on localhost:8081
- open localhost:3000/umbraco
- login with username: foobar@gmail.com password: foobarfoobar


## with compose and traefik it works
- docker compose build
- docker compose up
- localhost/umbraco -> foobar@gmail.com password: foobarfoobar
-> so the problem is the nuxt proxy