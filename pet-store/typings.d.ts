declare var process: Process

interface Process {
  env: Env
}

interface Env {
  STRIPE_KEY: string
  API_URL: string
  STRIPE_RETURN_BASE_URL: string
}

interface GlobalEnvironment {
  process: Process
}
