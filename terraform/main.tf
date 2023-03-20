#------------------------------------------------------------------------
# Purview Configuration
#------------------------------------------------------------------------

resource "purview_account" "purview" {
  name                = "tf-purview-test"
  resource_group_name = "terraform-purview-rg"
  location            = "Australia East"
}
