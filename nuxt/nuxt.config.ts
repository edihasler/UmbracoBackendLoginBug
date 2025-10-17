// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  compatibilityDate: "2025-07-15",
  devtools: { enabled: true },
  ssr: true,
  routeRules: {
    "/umbraco/**": {
      proxy: "http://localhost:8081/umbraco/**",
    },
  },
});
