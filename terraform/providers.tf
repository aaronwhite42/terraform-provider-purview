
terraform {
  required_providers {
    purview = {
      # copy binary to C:\Users\<username>\AppData\Roaming\terraform.d\plugins
      # or use this approach https://developer.hashicorp.com/terraform/cli/config/config-file#explicit-installation-method-configuration
      source  = "advocacy.dev/aaron/purview"
      version = "0.0.1"
    }
  }
}


provider "purview" {
  subscription_id = "ea97265d-1346-44b0-a86e-523b61d28340"
}

